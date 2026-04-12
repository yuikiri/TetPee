Bắt đầu làm 1 ứng dụng như thế nào:

đầu tiên phải biết là muốn làm cái app gì
- Tôi muốn làm về 1 app mua bán quần
- Bán cái gì: quần áo, giày dép, phụ kiện

Những ai sẽ tương tác với cái app này
- Người mua
- Người bán
- Admin

Có những thực thể nào tồn tại trong cái app
- Người mua
- Người bán
- Admin
- Sản phẩm
- Đơn hàng
- Giỏ hàng

Vậy thì những thực thể này có mối quan hệ gì với nhau
- Người bán có thể có nhiều sản phẩm muốn bán
- Người mua có thể có nhiều đơn hàng
- Một đơn hàng thì có thể có nhiều sản phầm
- Một sản phẩm có thể thuộc nhiều đơn hàng

Mình làm rõ hơn về các thực thể này (có những field gì)
- Người mua: id, name, email, password, address
- Sản phẩm: id, name, description, price, stock, seller_id

Nhờ nhugnwx nghiệp vụ trênthifif chúng ta có thể xác định đc những thứ:
- Databse cho hệ thống
- API cần thiết để phục vụ cho app này