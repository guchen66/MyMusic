using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.System.Services.MainSign.EmptyPlayListSign
{
    public class NeteasePlay : IMusicPlay
    {
        public Task PlayAll()
        {
            throw new NotImplementedException();
        }

        public Task PlayByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task PlayByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task StopAll()
        {
            throw new NotImplementedException();
        }

        public Task StopByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task StopByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
