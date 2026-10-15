namespace NPL_Portifolio01.Services;

/// <summary>Interface labels (buttons, headings…) in English and Vietnamese. Personal content lives in wwwroot/data/portfolio.json.</summary>
public static class UiText
{
    private static readonly Dictionary<string, (string En, string Vi)> Strings = new()
    {
        ["nav.home"] = ("Home", "Trang chủ"),
        ["nav.about"] = ("About", "Giới thiệu"),
        ["nav.skills"] = ("Skills", "Kỹ năng"),
        ["nav.experience"] = ("Experience", "Kinh nghiệm"),
        ["nav.projects"] = ("Projects", "Dự án"),
        ["nav.contact"] = ("Contact", "Liên hệ"),
        ["nav.primary"] = ("Main navigation", "Điều hướng chính"),
        ["nav.skip"] = ("Skip to content", "Bỏ qua đến nội dung"),
        ["nav.open"] = ("Open menu", "Mở menu"),
        ["nav.close"] = ("Close menu", "Đóng menu"),
        ["lang.switch"] = ("Chuyển sang tiếng Việt", "Switch to English"),
        ["theme.light"] = ("Switch to light theme", "Chuyển sang giao diện sáng"),
        ["theme.dark"] = ("Switch to dark theme", "Chuyển sang giao diện tối"),
        ["social"] = ("Social links", "Mạng xã hội"),

        ["hero.greeting"] = ("Hi there, I'm", "Xin chào, tôi là"),
        ["hero.viewWork"] = ("View my work", "Xem dự án"),
        ["hero.contact"] = ("Let's talk", "Liên hệ với tôi"),
        ["hero.cv"] = ("Download CV", "Tải CV"),
        ["hero.scroll"] = ("Scroll down", "Cuộn xuống"),

        ["about.title"] = ("Get to know me", "Đôi nét về tôi"),
        ["about.subtitle"] = ("Who I am, what I do and what I care about.", "Tôi là ai, tôi làm gì và tôi quan tâm đến điều gì."),

        ["skills.title"] = ("Skills & tools", "Kỹ năng & công cụ"),
        ["skills.subtitle"] = ("The technologies I use to turn ideas into working products.", "Những công nghệ tôi dùng để biến ý tưởng thành sản phẩm hoàn chỉnh."),
        ["skills.toolbox"] = ("Also in my toolbox", "Công cụ khác tôi sử dụng"),
        ["skills.level.expert"] = ("Expert", "Chuyên sâu"),
        ["skills.level.advanced"] = ("Advanced", "Nâng cao"),
        ["skills.level.intermediate"] = ("Intermediate", "Khá"),
        ["skills.level.familiar"] = ("Familiar", "Cơ bản"),

        ["experience.title"] = ("My journey so far", "Hành trình của tôi"),
        ["experience.subtitle"] = ("Roles, teams and studies that shaped how I work.", "Những vị trí, đội ngũ và quá trình học tập đã định hình cách tôi làm việc."),
        ["experience.work"] = ("Work", "Công việc"),
        ["experience.education"] = ("Education", "Học vấn"),

        ["projects.title"] = ("Things I've built", "Dự án tôi đã thực hiện"),
        ["projects.subtitle"] = ("A selection of personal and professional work. Open a card to see the details.", "Một số dự án cá nhân và công việc tiêu biểu. Mở từng thẻ để xem chi tiết."),
        ["projects.all"] = ("All", "Tất cả"),
        ["projects.filter"] = ("Filter projects", "Lọc dự án"),
        ["projects.details"] = ("View details", "Xem chi tiết"),
        ["projects.code"] = ("Source code", "Mã nguồn"),
        ["projects.demo"] = ("Live demo", "Bản demo"),
        ["projects.highlights"] = ("Highlights", "Điểm nổi bật"),
        ["projects.tech"] = ("Tech stack", "Công nghệ sử dụng"),
        ["projects.featured"] = ("Featured", "Nổi bật"),
        ["projects.close"] = ("Close", "Đóng"),

        ["contact.title"] = ("Let's build something together", "Cùng nhau tạo nên điều tuyệt vời"),
        ["contact.subtitle"] = ("Have a project, an opportunity or just want to say hi? My inbox is always open.", "Bạn có dự án, cơ hội hợp tác hay chỉ muốn chào hỏi? Hộp thư của tôi luôn rộng mở."),
        ["contact.email"] = ("Email", "Email"),
        ["contact.phone"] = ("Phone", "Điện thoại"),
        ["contact.location"] = ("Location", "Địa điểm"),
        ["contact.copy"] = ("Copy email address", "Sao chép địa chỉ email"),
        ["contact.copied"] = ("Copied!", "Đã sao chép!"),
        ["contact.form.title"] = ("Send me a message", "Gửi lời nhắn cho tôi"),
        ["contact.form.name"] = ("Your name", "Họ và tên"),
        ["contact.form.email"] = ("Your email", "Email của bạn"),
        ["contact.form.message"] = ("Message", "Lời nhắn"),
        ["contact.form.namePlaceholder"] = ("Jane Doe", "Nguyễn Văn A"),
        ["contact.form.messagePlaceholder"] = ("Tell me a little about your project…", "Hãy chia sẻ đôi chút về dự án của bạn…"),
        ["contact.form.send"] = ("Send message", "Gửi lời nhắn"),
        ["contact.form.sending"] = ("Sending…", "Đang gửi…"),
        ["contact.form.sent"] = ("Thank you! Your message has been sent.", "Cảm ơn bạn! Lời nhắn đã được gửi."),
        ["contact.form.opened"] = ("Opening your email app…", "Đang mở ứng dụng email của bạn…"),
        ["contact.form.error"] = ("Something went wrong. Please email me directly.", "Đã có lỗi xảy ra. Vui lòng gửi email trực tiếp cho tôi."),
        ["contact.form.required"] = ("Please fill in every field.", "Vui lòng điền đầy đủ các trường."),
        ["contact.form.invalidEmail"] = ("Please enter a valid email address.", "Vui lòng nhập địa chỉ email hợp lệ."),
        ["contact.form.mailtoNote"] = ("Your email app will open with the message pre-filled.", "Ứng dụng email sẽ mở với nội dung đã được điền sẵn."),

        ["footer.rights"] = ("All rights reserved.", "Bảo lưu mọi quyền."),
        ["footer.built"] = ("Built with Blazor WebAssembly · Hosted on GitHub Pages", "Xây dựng bằng Blazor WebAssembly · Triển khai trên GitHub Pages"),
        ["footer.top"] = ("Back to top", "Lên đầu trang"),

        ["notfound.title"] = ("Page not found", "Không tìm thấy trang"),
        ["notfound.text"] = ("The page you're looking for doesn't exist or has been moved.", "Trang bạn đang tìm không tồn tại hoặc đã được di chuyển."),
        ["notfound.home"] = ("Back to home", "Về trang chủ"),

        ["error.title"] = ("Couldn't load the portfolio content", "Không thể tải nội dung portfolio"),
        ["error.text"] = ("Check that wwwroot/data/portfolio.json exists and contains valid JSON.", "Hãy kiểm tra file wwwroot/data/portfolio.json có tồn tại và đúng định dạng JSON."),
        ["error.reload"] = ("Reload", "Tải lại"),
    };

    public static string Get(string key, string language) =>
        Strings.TryGetValue(key, out var text) ? (language == "vi" ? text.Vi : text.En) : key;
}
