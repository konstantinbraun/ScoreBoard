using IdentitySample.Entities;
using IdentitySample.Repository;

namespace IdentitySample.Service
{
    public class GroupService: EntityService<Group>, IGroupService
    {
        IGroupRepository _groupRepository;

        public GroupService(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
            : base(unitOfWork, groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Group GetById(int Id)
        {
            return _groupRepository.GetById(Id);
        }


        public System.Collections.Generic.IEnumerable<Group> GetSports()
        {
            return _groupRepository.FindBy(x=>!x.IsPublicGroup);
        }
    }
}
