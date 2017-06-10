using BlueService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlueService.Migrations
{
    class Configuration : CreateDatabaseIfNotExists<BlueServiceDataContext>
    {
        protected override void Seed(BlueServiceDataContext context)
        {
            User u = new Models.User() { Email = "ism.kundakci@hotmail.com", Name = "ismail kundakcı", Password = "202cb962ac59075b964b07152d234b70", Department = "bilgisayar programcılığı", ProfileImage = "https://qph.ec.quoracdn.net/main-qimg-25c48fa22c86b8f222445f578d911536", Token = Extras.GetMd5Hash("ism.kundakci@hotmail.com" + "202cb962ac59075b964b07152d234b70") };
            context.Products.Add(new Product() { Title = "Satılık uçlu kalem", Price = 1.5, User = u, ImageUrl = "http://cdn.avansas.com/assets/62051/rotring-300-versatil-uclu-kalem-0-5-mm-siyah-0-zoom.jpg" });
            context.Products.Add(new Product() { Title = "Ders notları matematik", Price = 10, User = u, ImageUrl = "http://acikliseokulu.com/wp-content/uploads/2017/01/classnotes.jpg" });
            context.Products.Add(new Product() { Title = "Vespa motosiklet", Price = 3000, User = u, ImageUrl = "http://jdata.vespa.com/mediaObject/vespa-html/img-modello/600x600resized/Sprint_600x600/original/Sprint_600x600.jpg" });
            context.Products.Add(new Product() { Title = "Mavi koltuk", Price = 1500,  User = u, ImageUrl = "https://www.yildizmobilya.com.tr/pictures/20151013204128_20.jpg" });
            context.Products.Add(new Product() { Title = "Spor ayakkabı", Price = 1.5,  User = u, ImageUrl = "https://img-flo.mncdn.com/mnresize/292/292/media/catalog/product/1/0/100268186_1.jpg" });
            base.Seed(context);
        }
    }
}