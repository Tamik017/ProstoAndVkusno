$(document).ready(function () {
    $(".edit-product-btn").click(function () {
        var productCard = $(this).closest('.user-card');
        var productId = productCard.data('product-id');

        $.get('/Admin/GetProduct', { productId: productId }, function (data) {
            // Очищаем предыдущие значения
            productCard.find('.user-info').each(function () {
                $(this).empty();
            });

            // Заполняем поля с новыми данными
            productCard.find('.user-info[data-field="Name"]').html('<input type="text" value="' + data.name + '"/>');
            productCard.find('.user-info[data-field="ShortDesc"]').html('<input type="text" value="' + data.shortDesc + '"/>');
            productCard.find('.user-info[data-field="LongDesc"]').html('<textarea>' + data.longDesc + '</textarea>');
            productCard.find('.user-info[data-field="img"]').html('<input type="text" value="' + data.img + '"/>');
            productCard.find('.user-info[data-field="price"]').html('<input type="text" value="' + data.price + '"/>');
            productCard.find('.user-info[data-field="isFavourite"]').html('<input type="checkbox" ' + (data.isFavourite ? 'checked' : '') + '/>');
            productCard.find('.user-info[data-field="available"]').html('<input type="checkbox" ' + (data.available ? 'checked' : '') + '/>');
            productCard.find('.user-info[data-field="categoryID"]').html('<select><option value="' + data.categoryID + '">' + data.category.Name + '</option></select>');

            productCard.find('.edit-product-btn').hide();
            productCard.find('.save-product-btn').show();
        });
    });

    $(".save-product-btn").click(function () {
        var productCard = $(this).closest('.user-card');
        var productId = productCard.data('product-id');

        // Сбор данных с формы
        var name = productCard.find('.user-info[data-field="Name"] input').val();
        var shortDesc = productCard.find('.user-info[data-field="ShortDesc"] input').val();
        var longDesc = productCard.find('.user-info[data-field="LongDesc"] textarea').val();
        var img = productCard.find('.user-info[data-field="img"] input').val();
        var price = parseFloat(productCard.find('.user-info[data-field="price"] input').val());
        var isFavourite = productCard.find('.user-info[data-field="isFavourite"] input').is(':checked');
        var available = productCard.find('.user-info[data-field="available"] input').is(':checked');
        var categoryId = productCard.find('.user-info[data-field="categoryID"] select').val();

        // Отправка данных на сервер
        $.post('/Admin/EditProduct', { ID: productId, Name: name, ShortDesc: shortDesc, LongDesc: longDesc, img: img, price: price, isFavourite: isFavourite, available: available, categoryID: categoryId }, function () {
            // Обновляем данные в продуктовой карточке
            updateProductCard(productCard, productId, name, shortDesc, longDesc, img, price, isFavourite, available, categoryId);

            productCard.find('.edit-product-btn').show();
            productCard.find('.save-product-btn').hide();
        });
    });

    // Функция обновления карточки продукта
    function updateProductCard(productCard, productId, name, shortDesc, longDesc, img, price, isFavourite, available, categoryId) {
        productCard.find('.user-info[data-field="Name"]').text(name);
        productCard.find('.user-info[data-field="ShortDesc"]').text(shortDesc);
        productCard.find('.user-info[data-field="LongDesc"]').text(longDesc);
        productCard.find('.user-info[data-field="img"]').text(img);
        productCard.find('.user-info[data-field="price"]').text(price);
        productCard.find('.user-info[data-field="isFavourite"] input').prop('checked', isFavourite);
        productCard.find('.user-info[data-field="available"] input').prop('checked', available);
        productCard.find('.user-info[data-field="categoryID"]').text(categoryId);
    }
});