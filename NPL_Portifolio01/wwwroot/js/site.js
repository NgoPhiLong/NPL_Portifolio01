// Small browser helpers used by the Blazor app (exposed as window.npl).
(() => {
    'use strict';

    const root = document.documentElement;
    const THEME_KEY = 'npl-theme';
    const LANGUAGE_KEY = 'npl-lang';
    const THEME_COLORS = { dark: '#070a13', light: '#f6f7fb' };

    const storage = {
        set(key, value) {
            try { localStorage.setItem(key, value); } catch { /* storage disabled */ }
        }
    };

    const reducedMotion = () => window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    let siteRef = null;
    let initialized = false;
    let ticking = false;
    let activeSection;
    let revealObserver = null;

    function updateScrollState() {
        ticking = false;
        const y = window.scrollY;
        const viewport = window.innerHeight;
        const max = root.scrollHeight - viewport;

        root.style.setProperty('--scroll-progress', max > 0 ? Math.min(y / max, 1).toFixed(4) : '0');
        root.classList.toggle('is-scrolled', y > 12);
        root.classList.toggle('show-back-to-top', y > viewport * 0.8);

        const sections = document.querySelectorAll('[data-nav-section]');
        let current = null;
        if (sections.length > 0) {
            if (max > 0 && y >= max - 4) {
                current = sections[sections.length - 1].id;
            } else {
                const line = viewport * 0.35;
                for (const section of sections) {
                    if (section.getBoundingClientRect().top <= line) {
                        current = section.id;
                    }
                }
            }
        }

        if (current !== activeSection) {
            activeSection = current;
            siteRef?.invokeMethodAsync('SetActiveSection', current).catch(() => { });
        }
    }

    function requestScrollUpdate() {
        if (!ticking) {
            ticking = true;
            requestAnimationFrame(updateScrollState);
        }
    }

    function observeReveals(node) {
        if (!revealObserver || node.nodeType !== Node.ELEMENT_NODE) {
            return;
        }
        if (node.matches('.reveal:not(.is-visible)')) {
            revealObserver.observe(node);
        }
        node.querySelectorAll('.reveal:not(.is-visible)').forEach(el => revealObserver.observe(el));
    }

    function setupReveal() {
        if (reducedMotion() || !('IntersectionObserver' in window)) {
            return;
        }

        root.classList.add('js-reveal');
        revealObserver = new IntersectionObserver(entries => {
            for (const entry of entries) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('is-visible');
                    revealObserver.unobserve(entry.target);
                }
            }
        }, { threshold: 0.1, rootMargin: '0px 0px -6% 0px' });

        observeReveals(document.body);

        // Blazor adds elements over time (language switch, filters, tabs…): watch for them.
        new MutationObserver(mutations => {
            for (const mutation of mutations) {
                mutation.addedNodes.forEach(observeReveals);
            }
            requestScrollUpdate();
        }).observe(document.body, { childList: true, subtree: true });
    }

    async function copyText(text) {
        try {
            await navigator.clipboard.writeText(text);
            return true;
        } catch {
            const area = document.createElement('textarea');
            area.value = text;
            area.setAttribute('readonly', '');
            area.style.position = 'fixed';
            area.style.opacity = '0';
            document.body.appendChild(area);
            area.select();
            let copied = false;
            try { copied = document.execCommand('copy'); } catch { /* not supported */ }
            area.remove();
            return copied;
        }
    }

    window.npl = {
        getPreferences() {
            return {
                language: root.lang === 'vi' ? 'vi' : 'en',
                theme: root.dataset.theme === 'light' ? 'light' : 'dark',
                reducedMotion: reducedMotion()
            };
        },

        setTheme(theme) {
            root.dataset.theme = theme;
            root.style.colorScheme = theme;
            storage.set(THEME_KEY, theme);
            document.querySelector('meta[name="theme-color"]')?.setAttribute('content', THEME_COLORS[theme] ?? THEME_COLORS.dark);
        },

        setLanguage(language) {
            root.lang = language;
            storage.set(LANGUAGE_KEY, language);
        },

        init(dotNetRef) {
            siteRef = dotNetRef;
            if (!initialized) {
                initialized = true;
                window.addEventListener('scroll', requestScrollUpdate, { passive: true });
                window.addEventListener('resize', requestScrollUpdate, { passive: true });
                setupReveal();
            }
            activeSection = undefined;
            requestScrollUpdate();
        },

        dispose() {
            siteRef = null;
        },

        scrollToSection(id, smooth = true) {
            const target = document.getElementById(id);
            if (!target) {
                return false;
            }
            const behavior = smooth && !reducedMotion() ? 'smooth' : 'instant';
            if (id === 'home') {
                window.scrollTo({ top: 0, behavior });
            } else {
                target.scrollIntoView({ behavior, block: 'start' });
            }
            return true;
        },

        scrollToTop() {
            window.scrollTo({ top: 0, behavior: reducedMotion() ? 'instant' : 'smooth' });
        },

        copyText,

        initDialog(dialog, dotNetRef) {
            dialog.addEventListener('close', () => {
                dotNetRef.invokeMethodAsync('OnDialogClosed').catch(() => { });
            });
            // A click whose target is the <dialog> itself happened on the backdrop.
            dialog.addEventListener('click', event => {
                if (event.target === dialog) {
                    dialog.close();
                }
            });
        },

        showDialog(dialog) {
            if (dialog && !dialog.open) {
                dialog.showModal();
                dialog.scrollTop = 0;
            }
        },

        closeDialog(dialog) {
            if (dialog?.open) {
                dialog.close();
            }
        }
    };
})();
