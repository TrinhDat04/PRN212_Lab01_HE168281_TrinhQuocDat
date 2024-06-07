using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository iAccoutRepository;
        public AccountMember GetAccountById(string accountID) => iAccoutRepository.GetAccountById(accountID);
    }
}
