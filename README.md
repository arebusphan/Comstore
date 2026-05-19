# Comstore

## Ý tưởng dự án

**Comstore** là một website bán máy tính chính hãng, hướng tới trải nghiệm mua sắm công nghệ thông minh bằng cách tích hợp AI tư vấn sản phẩm.

### Mục tiêu
*
- Giúp người dùng chọn máy tính phù hợp nhanh chóng.
- Cung cấp danh mục sản phẩm rõ ràng, dễ lọc, dễ so sánh.
- Tích hợp AI để tư vấn theo nhu cầu và ngân sách.
- Hỗ trợ khách hàng đưa ra quyết định bằng đánh giá và so sánh.

## Kế hoạch thực hiện chi tiết

### Bước 1: Phân tích yêu cầu

- Xác định đối tượng người dùng: học sinh, sinh viên, nhân viên văn phòng, lập trình viên, gamer.
- Xác định dữ liệu cần lưu:
  - Sản phẩm: tên, loại, CPU, RAM, GPU, ổ cứng, màn hình, giá, thương hiệu, mô tả, hình ảnh.
  - Người dùng: thông tin đăng nhập cơ bản, địa chỉ, lịch sử mua hàng.
  - Đơn hàng: sản phẩm, số lượng, tổng tiền, trạng thái.
  - Đánh giá: đánh giá sao, bình luận, sản phẩm liên quan.
  - Tư vấn AI: yêu cầu, ngân sách, loại sử dụng, kết quả đề xuất.

### Bước 2: Thiết kế kiến trúc

- Backend `.NET` tổ chức theo 4 lớp:
  - `Data`: chứa entity model và `ComStoreDbContext`.
  - `Repository`: truy xuất dữ liệu từ database.
  - `Service`: xử lý nghiệp vụ, logic tư vấn AI.
  - `API`: controller cung cấp endpoint cho frontend.
- Frontend:
  - Có thể dùng React, Vue hoặc Razor Pages.
  - Hiển thị danh mục sản phẩm, form AI tư vấn, trang chi tiết, giỏ hàng.
- AI tư vấn:
  - Dựa trên rule-based ban đầu.
  - Sau này có thể mở rộng bằng dịch vụ AI/ChatGPT.

### Bước 3: Triển khai backend

- Tạo và cấu hình `ComStoreDbContext`.
- Định nghĩa các entity với các thuộc tính chính:
  - `Product`
    - `Id`
    - `Name`
    - `CategoryId`
    - `Brand`
    - `Price`
    - `CPU`
    - `RAM`
    - `GPU`
    - `Storage`
    - `ScreenSize`
    - `Resolution`
    - `Battery`
    - `Description`
    - `ImageUrl`
    - `StockQuantity`
  - `Category`
    - `Id`
    - `Name`
    - `Description`
  - `Order`
    - `Id`
    - `UserId`
    - `OrderDate`
    - `TotalAmount`
    - `Status`
    - `ShippingAddress`
    - `PaymentMethod`
  - `OrderItem`
    - `Id`
    - `OrderId`
    - `ProductId`
    - `Quantity`
    - `UnitPrice`
  - `Review`
    - `Id`
    - `ProductId`
    - `UserId`
    - `Rating`
    - `Comment`
    - `CreatedAt`
  - `User`
    - `Id`
    - `FullName`
    - `Email`
    - `PasswordHash`
    - `Phone`
    - `Address`
  - `AICounselRequest`
    - `Id`
    - `UserId`
    - `UsageType`
    - `Budget`
    - `Preferences`
    - `SuggestedProducts`
    - `CreatedAt`
- Triển khai repository:
  - `ProductRepository`, `OrderRepository`, `ReviewRepository`, `UserRepository`.
- Triển khai service:
  - `ProductService`, `OrderService`, `ReviewService`, `AICounselService`.
- Viết API controller:
  - `ProductController`, `OrderController`, `ReviewController`, `AICounselController`, `UserController`.

### Bước 4: Thiết kế API chi tiết

#### User APIs

- `POST /api/users/register`
  - Tạo tài khoản người dùng mới.
  - Request body:
    - `fullName`
    - `email`
    - `password`
    - `phone` (tùy chọn)
    - `address` (tùy chọn)
  - Response:
    - `userId`
    - `email`
    - `createdAt`
- `POST /api/users/login`
  - Xác thực người dùng.
  - Request body:
    - `email`
    - `password`
  - Response:
    - `token`
    - `userId`
    - `email`
- `GET /api/users/{id}`
  - Lấy thông tin người dùng.
  - Response:
    - `id`, `fullName`, `email`, `phone`, `address`.
- `PUT /api/users/{id}`
  - Cập nhật thông tin người dùng.
  - Request body:
    - `fullName`, `phone`, `address`.
- `DELETE /api/users/{id}`
  - Xóa tài khoản người dùng (administrative / soft delete nếu cần).

#### Category APIs

- `GET /api/categories`
  - Lấy danh sách danh mục sản phẩm.
- `GET /api/categories/{id}`
  - Lấy chi tiết danh mục.
- `POST /api/categories`
  - Tạo danh mục mới.
  - Request body:
    - `name`, `description`.
- `PUT /api/categories/{id}`
  - Cập nhật danh mục.
- `DELETE /api/categories/{id}`
  - Xóa danh mục.

#### Product APIs

- `GET /api/products`
  - Lấy danh sách sản phẩm, hỗ trợ lọc theo categoryId, brand, priceRange, cpu, ram, gpu, search text.
  - Hỗ trợ paging: `page`, `pageSize`.
- `GET /api/products/{id}`
  - Lấy chi tiết một sản phẩm.
- `POST /api/products`
  - Tạo sản phẩm mới.
  - Request body:
    - `name`, `categoryId`, `brand`, `price`, `cpu`, `ram`, `gpu`, `storage`, `screenSize`, `resolution`, `battery`, `description`, `imageUrl`, `stockQuantity`.
- `PUT /api/products/{id}`
  - Cập nhật sản phẩm.
- `DELETE /api/products/{id}`
  - Xóa sản phẩm.
- `GET /api/products/compare?ids=1,2,3`
  - So sánh nhiều sản phẩm cùng lúc.

#### AI tư vấn

- `POST /api/ai-advice`
  - Gửi yêu cầu tư vấn AI.
  - Request body:
    - `userId` (tùy chọn nếu đã đăng nhập)
    - `usageType`: học tập, lập trình, gaming, đồ họa, văn phòng.
    - `budget`: ngân sách tối đa.
    - `preferredBrand` (tùy chọn)
    - `priority`: `performance`, `portability`, `budget`.
    - `additionalNotes` (tùy chọn).
  - Response:
    - `recommendedProducts`: danh sách sản phẩm gợi ý.
    - `explanation`: lý do chọn.
    - `topConfiguration`: cấu hình sản phẩm đề xuất.
- `GET /api/ai-advice/history?userId=123`
  - Lấy lịch sử yêu cầu tư vấn AI của người dùng.

#### Order APIs

- `POST /api/orders`
  - Tạo đơn hàng mới từ giỏ hàng.
  - Request body:
    - `userId`, `shippingAddress`, `paymentMethod`, `items`.
    - `items`: mảng `{ productId, quantity }`.
  - Response:
    - `orderId`, `orderDate`, `status`, `totalAmount`.
- `GET /api/orders/{id}`
  - Lấy chi tiết đơn hàng.
- `GET /api/orders?userId=123`
  - Lấy lịch sử đơn hàng người dùng.
- `PUT /api/orders/{id}`
  - Cập nhật trạng thái đơn hàng (ví dụ: `Pending`, `Confirmed`, `Shipped`, `Completed`).
- `DELETE /api/orders/{id}`
  - Hủy đơn hàng nếu chưa xử lý.

#### Review APIs

- `POST /api/reviews`
  - Thêm đánh giá sản phẩm.
  - Request body:
    - `productId`, `userId`, `rating`, `comment`.
- `GET /api/products/{id}/reviews`
  - Lấy đánh giá cho sản phẩm.
- `GET /api/users/{id}/reviews`
  - Lấy đánh giá của người dùng.
- `PUT /api/reviews/{id}`
  - Cập nhật đánh giá.
- `DELETE /api/reviews/{id}`
  - Xóa đánh giá.

### Bước 5: Xây dựng tính năng AI tư vấn

- Tạo form hỏi người dùng:
  - Mục đích sử dụng.
  - Ngân sách.
  - Ưu tiên hiệu năng hay giá rẻ.
  - Thương hiệu mong muốn.
- Viết logic tư vấn ban đầu:
  - So sánh nhu cầu với danh sách sản phẩm.
  - Chọn sản phẩm phù hợp nhất theo ngân sách và mục đích.
- Hiển thị kết quả: 2-3 gợi ý, giải thích ưu/nhược điểm.

### Bước 6: Triển khai frontend

- Trang chủ: banner, danh mục sản phẩm nổi bật.
- Trang danh mục: bộ lọc, tìm kiếm, hiển thị sản phẩm.
- Trang sản phẩm: chi tiết thông số, đánh giá, nút mua.
- Trang AI tư vấn: form thu thập yêu cầu và hiển thị gợi ý.
- Trang giỏ hàng/đặt hàng: xem tổng, nhập thông tin, hoàn tất.
- Trang so sánh: hiển thị trực quan các sản phẩm được chọn.

### Bước 7: Kiểm thử

- Kiểm thử backend:
  - API trả dữ liệu chính xác.
  - Logic lọc, so sánh và AI tư vấn đúng.
- Kiểm thử frontend:
  - Form AI hoạt động.
  - Lọc và so sánh hiển thị đúng.
  - Quy trình giỏ hàng và đặt hàng chạy mượt.
- Kiểm thử tích hợp:
  - Frontend gọi API ổn định.
  - Dữ liệu nhập/xuất đồng bộ giữa các lớp.

### Bước 8: Hoàn thiện và triển khai

- Hoàn thiện tài liệu dự án và hướng dẫn chạy.
- Thiết lập `launchSettings.json` hoặc `DOTNET_URLS` để tránh xung đột port.
- Triển khai lên dịch vụ hosting hoặc server.
- Nếu cần, bổ sung chatbot AI và hệ thống khuyến mãi.

## Lộ trình chi tiết

1. Chuẩn bị môi trường, kiểm tra solution hiện tại.
2. Thiết kế schema database và chạy migration.
3. Xây dựng API sản phẩm cơ bản.
4. Xây dựng API đặt hàng và đánh giá.
5. Xây dựng module AI tư vấn.
6. Xây dựng frontend hiển thị danh mục và AI.
7. Kiểm thử toàn bộ luồng.
8. Tinh chỉnh UI/UX và triển khai.

## Gợi ý công nghệ

- Backend: .NET 9, ASP.NET Core Web API, Entity Framework Core.
- Database: SQL Server / SQLite / PostgreSQL.
- Frontend: React, Vue, hoặc Razor Pages.
- AI: rule-based ban đầu, mở rộng bằng API AI (OpenAI, Azure AI, v.v.).

## Cấu trúc đề xuất trong solution

- `Data/`
  - `ComStoreDbContext.cs`
  - `User.cs`
  - `Product.cs`
  - `Order.cs`
  - `Review.cs`
- `Repository/`
  - `UserRepository.cs`
  - `UserRepository.cs`
  - `ProductRepository.cs`
- `Service/`
  - `UserService.cs`
  - `UserService.cs`
  - `ProductService.cs`
  - `AICounselService.cs`
- `API/`
  - `Controllers/ProductController.cs`
  - `Controllers/OrderController.cs`
  - `Controllers/ReviewController.cs`
  - `Controllers/AICounselController.cs`

---

## Ghi chú

- Bắt đầu bằng các API cơ bản rồi mở rộng dần.
- Nếu muốn nhanh, có thể dùng dữ liệu mẫu trước khi kết nối database thật.
- Tập trung UX AI tư vấn đơn giản, hữu dụng và dễ hiểu.
 
