using static Entities.MediaBase;


namespace BusinessLogicalLayer.ApiConsumer
{
    public class Convertercate
    {
        public static Genre CovertiCatego(RootCate Cate)
        {
            if (Cate.data == null)
            {
                return new Genre();
            }

             var c = new Genre();
            //c.Name = Cate.data.attributes.title;
            //c.Description = Cate.data.attributes.description;
            //c.ID = Convert.ToInt32(Cate.data.id);


            return c;
        }
    }

}

