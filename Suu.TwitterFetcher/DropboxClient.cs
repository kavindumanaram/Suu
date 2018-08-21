using System;

namespace Suu.TwitterFetcher
{
    internal class DropboxClient : IDisposable
    {
        private string v;

        public DropboxClient(string v)
        {
            this.v = v;
        }
    }
}