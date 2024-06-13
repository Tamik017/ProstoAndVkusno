using ProstoAndVkusno.Data.Interfaces;
using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.Data.Mocks;

namespace ProstoAndVkusno.Data.mocks
{
    public class MockProducts : IAllProducts
    {
        private readonly IProductCategory _categoryProdutcs = new MockCategory();

        public IEnumerable<Product> GetProducts
        {
            get
            {
                return new List<Product>
                {
                    new Product
                    {
                        Name = "Барбекю",
                        ShortDesc = "Ароматная пицца с барбекю соусом",
                        LongDesc = "Нежная пицца с аппетитным соусом барбекю, сочной курицей, свежими овощами и сыром моцарелла. Идеальный выбор для любителей насыщенного вкуса и аромата. Наслаждайтесь каждой долькой!",
                        img = "/img/Барбекю.png",
                        price = 999,
                        isFavourite = true,
                        available = true,
                        Category =  _categoryProdutcs.GetCategories.First(c => c.Name == "Пицца")
                    },
                    new Product
                    {
                        Name = "Карбонара",
                        ShortDesc = "Классическая паста с беконом и сливочным соусом",
                        LongDesc = "Изысканная паста карбонара с нежным сливочным соусом, хрустящим беконом и пармезаном. Это блюдо перенесет вас в сердце Италии с каждым вкусным укусом. Идеально для уютного ужина.",
                        img = "/img/Карбонара.png",
                        price = 88,
                        isFavourite = true,
                        available = true,
                        Category =  _categoryProdutcs.GetCategories.First(c => c.Name == "Паста")
                    },
                    new Product
                    {
                        Name = "Чизбургер",
                        ShortDesc = "Сочный бургер с говядиной и сыром",
                        LongDesc = "Классический чизбургер с сочной говяжьей котлетой, расплавленным сыром чеддер, свежими овощами и специальным соусом в мягкой булочке. Погрузитесь в мир удовольствия от каждого укуса.",
                        img = "/img/Чизбургер.png",
                        price = 777,
                        isFavourite = true,
                        available = true,
                        Category =  _categoryProdutcs.GetCategories.First(c => c.Name == "Фастфуд")
                    },
                    new Product
                    {
                        Name = "Добрый кола",
                        ShortDesc = "Освежающий сок со вкусом колы",
                        LongDesc = "Попробуйте необычный сок со вкусом колы от 'Добрый'. Он освежает и бодрит, идеально подходит для утоления жажды в жаркий день или как дополнение к любимым блюдам. Наслаждайтесь уникальным вкусом!",
                        img = "/img/Кола.png",
                        price = 666,
                        isFavourite = true,
                        available = true,
                        Category =  _categoryProdutcs.GetCategories.First(c => c.Name == "Сок")
                    },
                    new Product
                    {
                        Name = "Сырный",
                        ShortDesc = "Вкусный сырный соус для закусок",
                        LongDesc = "Сырный соус, который сделает ваши закуски еще вкуснее. Насыщенный и ароматный, он идеально подходит для чипсов, начос или свежих овощей. Добавьте немного сыра в каждое блюдо и наслаждайтесь его удивительным вкусом.",
                        img = "/img/Сырный.png",
                        price = 555,
                        isFavourite = true,
                        available = true,
                        Category =  _categoryProdutcs.GetCategories.First(c => c.Name == "Соусы")
                    }
                };
            }

        }

        public IEnumerable<Product> GetFavProducts { get; set; }

        public Product GetProductById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
