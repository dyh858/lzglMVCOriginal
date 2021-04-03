using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DAL;
namespace BLL
{
    public class PostManager
    {
        public Post Show(Int32 id)
        {
            try
            {
                return new PostService().FindById(id);
            }
            catch (Exception ex)
            {                                    
                throw ex;
            }      
        }
        public List<Post> list()
        {
            try
            {
                return new PostService().FindAll(); 
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        public String getTable()
        {
            try
            {
                return new PostService().getTable();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
