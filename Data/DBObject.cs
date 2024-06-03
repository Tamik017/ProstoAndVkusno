using ProstoAndVkusno.Data.Models;
using ProstoAndVkusno.DBContext;

namespace ProstoAndVkusno.Data
{
    public class DBObject
    {
        public static void Initial(ApplicationContext content)
        {            
            if(!content._categories.Any())
            {
                content._categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!content._products.Any())
            {
                content.AddRange(
                    new Product
                    {
                        Name = "Барбекю",
                        ShortDesc = "Ароматная пицца с барбекю соусом",
                        LongDesc = "Нежная пицца с аппетитным соусом барбекю, сочной курицей, свежими овощами и сыром моцарелла. Идеальный выбор для любителей насыщенного вкуса и аромата. Наслаждайтесь каждой долькой!",
                        img = "/img/Барбекю.png",
                        price = 999,
                        isFavourite = true,
                        available = true,
                        Category = Categories["Пицца"]
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
                        Category = Categories["Паста"]
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
                        Category = Categories["Фастфуд"]
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
                        Category = Categories["Напитки"]
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
                        Category = Categories["Соусы"]
                    }
                );
            }

            content.SaveChanges();
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if(category == null)
                {
                    var list = new Category[]
                    {
                        new Category {Name = "Пицца", Description = "Вкусная пицца!"},
                        new Category {Name = "Паста", Description = "Вкусная паста!"},
                        new Category {Name = "Фастфуд", Description = "Вкусный фастфуд!"},
                        new Category {Name = "Напитки", Description = "Вкусные напитки!"},
                        new Category {Name = "Соусы", Description = "Вкусные соусы!"}
                    };

                    category = new Dictionary<string, Category>();
                    foreach (Category element in list) 
                    {
                        category.Add(element.Name, element);
                    }
                }

                return category;
            }
        }
    }
}
