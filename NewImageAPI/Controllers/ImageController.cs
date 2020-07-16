using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web.Http;

namespace NewImageAPI.Controllers
{
    public class ImageController : ApiController
    {
        private readonly ForAPIEntities context = new ForAPIEntities();

        // GET: api/Image
        public IEnumerable<ImageSerial> Get([FromUri]string Login, [FromUri]string Password)
        {
            IQueryable<user> user = this.context.user.Where(i => i.Login == Login && i.Password == Password);
            if (user.Count() > 0)
            {
                return this.context.Image.Where(i => i.UserID == user.FirstOrDefault().IDUser).Select(Obj => new ImageSerial { Id = Obj.Id, Header = Obj.Header,Image1 = Obj.Image1, UserID = Obj.UserID });
            }
            else
            {
                return null;
            }
        }

        // GET: api/Image/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Image
        public IHttpActionResult Post([FromBody]ImageSerial array, [FromUri]string Login, [FromUri]string Password)
        {
            IQueryable<user> user = this.context.user.Where(i => i.Login == Login && i.Password == Password);
            if (user.Count() > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    //ImageSerial serial = (ImageSerial)serializer.ReadObject(ms);
                    this.context.Image.Add(
                             new Image
                             {
                                 Header = array.Header,
                                 Image1 = array.Image1,
                                 UserID = array.UserID
                             });
                    this.context.SaveChanges();
                    return this.Ok();
                }
            }
            else
            {
                return this.NotFound();
            }
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ImageSerial);
        }

        // PUT: api/Image/5
        public void Put(int id, [FromBody]byte[] image)
        {

        }

        // DELETE: api/Image/5
        public void Delete(int id)
        {

        }
    }

    public partial class ImageSerial
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public byte[] Image1 { get; set; }
        public Nullable<long> UserID { get; set; }
    }
}
