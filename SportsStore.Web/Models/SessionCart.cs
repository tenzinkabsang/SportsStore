using Newtonsoft.Json;
using SportsStore.Data;
using SportsStore.Web.Infrastructure;

namespace SportsStore.Web.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession? Session { get; set; }

        private const string KEY = "cart";


        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;

            SessionCart cart = session?.GetJson<SessionCart>(KEY) ?? new();

            cart.Session = session;
            return cart;
        }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session?.SetJson(KEY, this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session?.SetJson(KEY, this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove(KEY);
        }
    }
}
