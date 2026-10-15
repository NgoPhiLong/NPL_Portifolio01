# NPL Portfolio

Website portfolio cá nhân của **Ngô Phi Long**. Trang web được viết bằng **Blazor WebAssembly (.NET 10)** và tự động deploy lên **GitHub Pages** bằng GitHub Actions.

🔗 **Live:** https://ngophilong.github.io/NPL_Portifolio01/

## Tính năng

- Trang one-page gồm các phần Hero, Giới thiệu, Kỹ năng, Kinh nghiệm/Học vấn, Dự án và Liên hệ.
- Song ngữ **English / Tiếng Việt**: có nút chuyển ngôn ngữ, và lần đầu vào trang sẽ tự chọn theo ngôn ngữ của trình duyệt.
- Giao diện **Dark / Light**: lần đầu theo cài đặt của hệ điều hành, lựa chọn sau đó được lưu trong `localStorage`.
- **Toàn bộ nội dung nằm trong một file JSON** nên không cần sửa code khi cập nhật thông tin.
- Hiệu ứng typing, reveal khi cuộn trang, scroll-spy trên thanh menu, bộ lọc và popup chi tiết dự án, form liên hệ.
- Responsive cho mobile, tablet và desktop. Có hỗ trợ accessibility: skip link, điều hướng bằng bàn phím, và tôn trọng tùy chọn `prefers-reduced-motion`.
- Có trang 404 riêng. Deep link (ví dụ `/#projects`) hoạt động trên GitHub Pages.

## Cấu trúc thư mục

```
.github/workflows/deploy.yml   # CI/CD: build và deploy lên GitHub Pages
NPL_Portifolio01/
├── wwwroot/
│   ├── data/portfolio.json    # ⭐ TOÀN BỘ NỘI DUNG CỦA WEBSITE
│   ├── css/app.css            # Design system (màu sắc, layout, animation)
│   ├── js/site.js             # Scroll-spy, reveal animation, dialog, clipboard
│   └── index.html             # Trang khung, màn hình loading, meta SEO
├── Sections/                  # Các section: Hero, About, Skills, Experience, Projects, Contact
├── Components/                # Các component dùng chung: Icon, ProjectCard, ProjectDialog, ...
├── Layout/                    # MainLayout, NavBar, SiteFooter
├── Services/
│   ├── SiteState.cs           # Ngôn ngữ, theme, dữ liệu, section đang active
│   └── UiText.cs              # Nhãn giao diện EN/VI (menu, nút, thông báo)
└── Models/                    # Schema của portfolio.json
```

## ✏️ Chỉnh sửa nội dung

Mọi thông tin hiển thị trên trang nằm trong file [`NPL_Portifolio01/wwwroot/data/portfolio.json`](NPL_Portifolio01/wwwroot/data/portfolio.json). Nội dung hiện tại là **dữ liệu mẫu**, bạn hãy thay bằng thông tin thật của mình.

### Văn bản song ngữ

Mọi trường văn bản đều nhận một trong hai dạng sau:

```jsonc
"tagline": "Same text for both languages",
"tagline": { "en": "English text", "vi": "Nội dung tiếng Việt" }
```

Nếu một ngôn ngữ bị thiếu, trang sẽ tự dùng ngôn ngữ còn lại. File cho phép viết comment (`//`) và dấu phẩy cuối danh sách.

### Các phần chính

| Khóa | Nội dung |
|---|---|
| `profile` | Tên, `initials`, `roles` (hiệu ứng typing), `tagline`, `availability` (để trống thì ẩn huy hiệu), `avatar`, `location`, `email`, `phone`, `cv`, `heroBadges` |
| `socials` | Danh sách `{ "name", "icon", "url" }`, dùng ở Hero, Liên hệ và Footer |
| `about` | `paragraphs`, `facts` (`icon`, `label`, `value`) và `stats` (`value` là số nguyên có hiệu ứng đếm, `suffix` như `+` hoặc `%`) |
| `skillGroups` | Nhóm kỹ năng. Kỹ năng có `level` (0–100) hiển thị dạng thanh tiến độ; không có `level` thì hiển thị dạng tag |
| `toolbox` | Danh sách công cụ khác, hiển thị dạng chip |
| `experience`, `education` | Timeline gồm `title`, `organization`, `url`, `period`, `location`, `description`, `highlights`, `tags`. Nếu một danh sách để trống thì nút chuyển tab sẽ ẩn |
| `projects` | `title`, `category` (dùng làm bộ lọc), `summary` (hiển thị trên thẻ), `description` và `highlights` (hiển thị trong popup), `tags`, `year`, `repoUrl`, `demoUrl`, `featured`, `icon` hoặc `image` |
| `contact` | `heading`, `text` (không bắt buộc) và `formspreeId` |

Muốn ẩn một section thì để trống dữ liệu của nó, ví dụ `"projects": []`. Section đó và mục menu tương ứng sẽ tự ẩn.

### Ảnh đại diện, ảnh dự án và CV

Đặt file vào `NPL_Portifolio01/wwwroot/` rồi khai báo bằng **đường dẫn tương đối, không có dấu `/` ở đầu**:

```jsonc
"avatar": "images/avatar.jpg",                        // hoặc "https://github.com/NgoPhiLong.png?size=460"
"cv": { "en": "files/cv-en.pdf", "vi": "files/cv-vi.pdf" },
// trong một project:
"image": "images/projects/taskflow.png"               // không có ảnh thì dùng "icon"
```

- Nếu `avatar` để trống, trang sẽ hiển thị chữ viết tắt (`initials`).
- Nếu `cv` để trống, nút tải CV sẽ ẩn.

### Icon

Các trường `icon` nhận tên lấy từ bộ [Feather Icons](https://feathericons.com). Các tên đang có sẵn:

`activity` `alert-circle` `arrow-right` `arrow-up` `arrow-up-right` `award` `bar-chart` `book` `book-open` `box` `briefcase` `calendar` `check` `check-circle` `cloud` `code` `coffee` `compass` `copy` `cpu` `database` `download` `external-link` `facebook` `folder` `git-branch` `github` `globe` `grid` `heart` `home` `info` `instagram` `layers` `layout` `linkedin` `mail` `map-pin` `menu` `message-circle` `monitor` `moon` `package` `pen-tool` `phone` `send` `server` `smartphone` `star` `sun` `target` `terminal` `tool` `trending-up` `twitter` `user` `x` `youtube` `zap`

Muốn thêm icon khác: copy phần nội dung bên trong thẻ `<svg>` từ feathericons.com vào `Components/IconPaths.cs`.

### Form liên hệ

- **Mặc định** (`formspreeId` để trống): khi gửi form, trang sẽ mở ứng dụng email của người xem và điền sẵn nội dung.
- **Gửi trực tiếp**: tạo form miễn phí tại [formspree.io](https://formspree.io), rồi điền ID của form (ví dụ `"xyzabcd"`) vào `contact.formspreeId`.

### Nhãn giao diện và màu sắc

- Chữ trên menu, nút bấm và thông báo nằm trong `Services/UiText.cs`.
- Màu thương hiệu, gradient và theme được khai báo bằng CSS variables ở đầu file `wwwroot/css/app.css` (`--brand-1`, `--brand-2`, `--brand-3`).

## 💻 Chạy trên máy

Cần cài [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
cd NPL_Portifolio01
dotnet watch
```

Sau đó mở http://localhost:5201. Mỗi khi sửa `portfolio.json`, chỉ cần tải lại trang để thấy thay đổi.

## 🚀 Deploy lên GitHub Pages

Workflow [`.github/workflows/deploy.yml`](.github/workflows/deploy.yml) tự động build và deploy mỗi khi có commit được push lên nhánh `master` hoặc `main`. Bạn cũng có thể chạy tay trong tab **Actions**.

Thiết lập một lần:

1. **Repository phải ở chế độ Public.** GitHub Pages cho repo Private chỉ dùng được với gói GitHub Pro/Team/Enterprise. Đổi chế độ tại *Settings → General → Danger Zone → Change visibility*.
2. Vào **Settings → Pages → Build and deployment → Source**, chọn **GitHub Actions**.
3. Push code lên. Sau khoảng 1–2 phút, trang sẽ có tại `https://<username>.github.io/<repo>/`.

Workflow tự xử lý các việc sau:

- Đặt `<base href>` đúng theo tên repo: dùng `/<repo>/` cho project site, và `/` cho repo `<username>.github.io` hoặc khi có file `wwwroot/CNAME` (custom domain).
- Tạo `404.html` để các đường dẫn sâu vẫn mở được app.
- Tạo `.nojekyll` để GitHub Pages phục vụ thư mục `_framework`.

## Credits

- Icons: [Feather Icons](https://feathericons.com) (MIT License)
- Fonts: [Be Vietnam Pro](https://fonts.google.com/specimen/Be+Vietnam+Pro) và [JetBrains Mono](https://fonts.google.com/specimen/JetBrains+Mono) (SIL Open Font License)
