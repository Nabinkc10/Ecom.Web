using ECom.Repository;
using ECom.Services;

namespace ECom.web
{
    public class Services
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Repository
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            #endregion

            #region Services
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            #endregion
        }
    }
}
