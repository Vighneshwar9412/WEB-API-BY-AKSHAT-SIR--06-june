using FourQT.Entities;
using FourQT.Entities.General;
using FourQT.Entities.Portal;

namespace MobAppCoreAPI.Interfaces.General
{
    public interface IValidateKey
    {
        public ResponseStatus<ValidateKey> validateKey(string mKey);
    }
}
