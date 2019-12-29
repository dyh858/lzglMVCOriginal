using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;


namespace BLL
{
    public class ApplicantManager
    {
        public bool add(Applicant SysApplicant)
        {
            return new ApplicantService().insert(SysApplicant);
        }
        public List<Applicant> list()
        {
            return new ApplicantService().list();
        }
        public Applicant show(int aid)
        {
            return new ApplicantService().findById(aid);
        }
        public bool Modify(Applicant vo)
        {
            return new ApplicantService().Modify(vo);
        }
        public bool Delete(int aid)
        {
            return new ApplicantService().delete(aid);
        }
    }
}
