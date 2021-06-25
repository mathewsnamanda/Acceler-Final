using mfiles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mfiles.Services
{
    public interface IBidCategory
    {
        IEnumerable<core_bid> GetCore_Bids(int applicant_id);
        core_bid core_bid(int categoryid, int bidid);
        void Addbid(int categoryod, core_bid core_Bid);
        void Updatebid(core_bid core_Bid);
        void Deletebid(core_bid core_Bid);

        IEnumerable<core_category> GetCore_Categories();
        core_category GetCore_Category(int categoryid);
        IEnumerable<core_category> GetCore_Categories(IEnumerable<int> categoryids);
        void addcategory(core_category core_Category);
        void updatecategory(core_category core_Category);

        void deletecategory(core_category core_Category);
        bool categoryexist(int categoryid);
        bool bidexist(int bidid);
        bool save();
    }
}
