using AutoMapper;

namespace CodeChallenge.Core.Automapper
{
    public class AutoMapperConfig
    {
        public static void SetUpConfiguration()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(AutoMapperConfig)));
        }
    }
}
