using AutoMapper;

namespace OnlineVoting.Mappings
{
    public interface IMapTo<T> 
    {
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType());
        }
    }
}
