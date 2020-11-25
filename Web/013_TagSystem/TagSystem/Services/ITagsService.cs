using System.Collections.Generic;
using TagSystem.Models.Tags;

namespace TagSystem.Services
{
    public interface ITagsService
    {
        string GetTag(string tagId);

        IEnumerable<TagEntity> Query(TagQueryEntity queryEntity);

        void Create(TagEntity tag);

        IEnumerable<TagEntity> GetTag(string shopId, string id);
    }
}