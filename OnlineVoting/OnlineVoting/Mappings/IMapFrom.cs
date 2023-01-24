using AutoMapper;

namespace OnlineVoting.Mappings
{
    public interface IMapFrom<T>
    {
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
