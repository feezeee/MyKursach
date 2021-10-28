using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class AuthorizedUser
    {
        private static AuthorizedUser instance;

        public bool _IsAutorized { get; private set; } = false;

        private AuthorizedUser()
        { }

        public static AuthorizedUser GetInstance()
        {
            if (instance == null)
                instance = new AuthorizedUser();
            return instance;
        }

        private Worker worker;

        public void SetUser(Worker worker)
        {
            if (!_IsAutorized)
            {
                this.worker = worker;
                _IsAutorized = true;
            }
            
        }

        public Worker GetWorker()
        {
            return worker;
        }

        public void ClearUser()
        {
            if (_IsAutorized)
            {
                worker = null;
                _IsAutorized = false;
            }
        }

    }
}
