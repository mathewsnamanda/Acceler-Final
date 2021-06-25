using mfiles.Db_Context;
using mfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mfiles.Services
{
    public class BidCategoryRepo : IBidCategory
    {
        public bidcategorycontext _Context { get; }

        public BidCategoryRepo(bidcategorycontext bidcategorycontext)
        {
            _Context = bidcategorycontext??throw new ArgumentNullException(nameof(bidcategorycontext));
        }

        
        public void Addbid(int categoryod, core_bid core_Bid)
        {
            if (categoryod <= 0)
            {
                throw new ArgumentNullException(nameof(categoryod));
            }
            if (core_Bid == null)
            {
                throw new ArgumentNullException(nameof(core_Bid));
            }
            core_Bid.category_id = categoryod;
            _Context.core_bid.Add(core_Bid);
        }

        public void addcategory(core_category core_Category)
        {
            if (core_Category == null)
            {
                throw new ArgumentNullException(nameof(core_Category));
            }
            _Context.core_category.Add(core_Category);
        }

        public bool bidexist(int bidid)
        {
            if (bidid <= 0)
            {
                throw new ArgumentNullException(nameof(bidid));
            }
            return _Context.core_bid.Any(a => a.id == bidid);
        }

        public bool categoryexist(int categoryid)
        {
            if (categoryid <= 0)
            {
                throw new ArgumentNullException(nameof(categoryid));
            }
            return _Context.core_category.Any(a => a.id==categoryid);
        }

        public core_bid core_bid(int categoryid, int bidid)
        {
            if (categoryid <= 0)
            {
                throw new ArgumentNullException(nameof(categoryid));
            }
            if (bidid <= 0)
            {
                throw new ArgumentNullException(nameof(bidid));
            }
            return _Context.core_bid.Where(a => a.id == bidid && a.category_id == categoryid).FirstOrDefault();
        }

        public void Deletebid(core_bid core_Bid)
        {
            if (core_Bid == null)
                throw new ArgumentNullException(nameof(core_bid));
            _Context.core_bid.Remove(core_Bid);

        }

        public void deletecategory(core_category core_Category)
        {
            if (core_Category == null)
                throw new ArgumentNullException(nameof(core_Category));

            _Context.core_category.Remove(core_Category);
        }

        public IEnumerable<core_bid> GetCore_Bids(int applicant_id)
        {
            if (applicant_id<=0)
                throw new ArgumentNullException(nameof(applicant_id));
            return _Context.core_bid.Where(a => a.applicant_id == applicant_id)
                 .OrderBy(a => a.id).ToList();
        }

        public IEnumerable<core_category> GetCore_Categories()
        {
            return _Context.core_category.ToList();
        }

        public IEnumerable<core_category> GetCore_Categories(IEnumerable<int> categoryids)
        {
         if(categoryids==null)
            {
                throw new ArgumentNullException(nameof(categoryids));
            }
            return _Context.core_category.Where(a => categoryids.Contains(a.id)).OrderBy(a=>a.name);
        }

        public core_category GetCore_Category(int categoryid)
        {
            if (categoryid <=0)
                throw new ArgumentNullException(nameof(categoryid));
            return _Context.core_category.FirstOrDefault(a => a.id == categoryid);
        }

        public bool save()
        {
            return (_Context.SaveChanges()>=0);
        }

        public void Updatebid(core_bid core_Bid)
        {
            //throw new NotImplementedException();
        }

        public void updatecategory(core_category core_Category)
        {
           // throw new NotImplementedException();
        }
    }
}
