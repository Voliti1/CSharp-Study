using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SCT_Form
{
    internal static class AccountService
    {
        public const string AdminLevel = "Admin";
        public const string GeneralLevel = "Normal";

        private static readonly JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();

        public static string AccountFilePath
        {
            get
            {
                return AppDataPaths.AccountFilePath;
            }
        }

        public static bool TryCreateAccount(string userId, string password, string passwordCheck, string userLevel, string userName, out string message)
        {
            message = string.Empty;
            userId = (userId ?? string.Empty).Trim();
            password = password ?? string.Empty;
            passwordCheck = passwordCheck ?? string.Empty;
            userLevel = NormalizeUserLevel(userLevel);
            userName = (userName ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(userId))
            {
                message = "ID를 입력하세요.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                message = "Password를 입력하세요.";
                return false;
            }

            if (password != passwordCheck)
            {
                message = "Password와 PW Check가 일치하지 않습니다.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userLevel))
            {
                message = "User Level을 선택하세요.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                message = "User Name을 입력하세요.";
                return false;
            }

            AccountStore store = LoadStore();
            if (store.Accounts.Any(account => string.Equals(account.UserId, userId, StringComparison.OrdinalIgnoreCase)))
            {
                message = "이미 존재하는 ID입니다.";
                return false;
            }

            store.Accounts.Add(new AccountInfo
            {
                UserId = userId,
                Password = password,
                UserLevel = userLevel,
                UserName = userName,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            SaveStore(store);
            message = "계정이 생성되었습니다.";
            return true;
        }

        public static bool TryFindIds(string userName, string userLevel, out List<string> userIds, out string message)
        {
            userIds = new List<string>();
            message = string.Empty;
            userName = (userName ?? string.Empty).Trim();
            userLevel = NormalizeUserLevel(userLevel);

            if (string.IsNullOrWhiteSpace(userName))
            {
                message = "User Name을 입력하세요.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userLevel))
            {
                message = "User Level을 선택하세요.";
                return false;
            }

            userIds = LoadStore().Accounts
                .Where(account =>
                    string.Equals(account.UserName, userName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(NormalizeUserLevel(account.UserLevel), userLevel, StringComparison.OrdinalIgnoreCase))
                .Select(account => account.UserId)
                .Where(userId => !string.IsNullOrWhiteSpace(userId))
                .ToList();

            if (userIds.Count == 0)
            {
                message = "일치하는 계정을 찾을 수 없습니다.";
                return false;
            }

            return true;
        }

        public static bool TryFindPassword(string userId, string userName, string userLevel, out string password, out string message)
        {
            password = string.Empty;
            message = string.Empty;
            userId = (userId ?? string.Empty).Trim();
            userName = (userName ?? string.Empty).Trim();
            userLevel = NormalizeUserLevel(userLevel);

            if (string.IsNullOrWhiteSpace(userId))
            {
                message = "ID를 입력하세요.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                message = "User Name을 입력하세요.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(userLevel))
            {
                message = "User Level을 선택하세요.";
                return false;
            }

            AccountInfo account = LoadStore().Accounts.FirstOrDefault(item =>
                string.Equals(item.UserId, userId, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(item.UserName, userName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(NormalizeUserLevel(item.UserLevel), userLevel, StringComparison.OrdinalIgnoreCase));

            if (account == null)
            {
                message = "일치하는 계정을 찾을 수 없습니다.";
                return false;
            }

            password = account.Password;
            return true;
        }

        public static bool TryLogin(string userId, string password, out AccountInfo account, out string message)
        {
            account = null;
            message = string.Empty;
            userId = (userId ?? string.Empty).Trim();
            password = password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(password))
            {
                message = "ID와 Password를 입력하세요.";
                return false;
            }

            account = LoadStore().Accounts.FirstOrDefault(item =>
                string.Equals(item.UserId, userId, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(item.Password, password, StringComparison.Ordinal));

            if (account == null)
            {
                message = "ID 또는 Password가 일치하지 않습니다.";
                return false;
            }

            account.UserLevel = NormalizeUserLevel(account.UserLevel);
            return true;
        }

        public static bool IsAdmin(AccountInfo account)
        {
            return account != null && string.Equals(NormalizeUserLevel(account.UserLevel), AdminLevel, StringComparison.OrdinalIgnoreCase);
        }

        public static string NormalizeUserLevel(string userLevel)
        {
            if (string.Equals(userLevel, AdminLevel, StringComparison.OrdinalIgnoreCase)) return AdminLevel;
            if (string.Equals(userLevel, "관리자 등급", StringComparison.OrdinalIgnoreCase)) return AdminLevel;
            if (string.Equals(userLevel, "Administrator", StringComparison.OrdinalIgnoreCase)) return AdminLevel;
            if (string.Equals(userLevel, GeneralLevel, StringComparison.OrdinalIgnoreCase)) return GeneralLevel;
            if (string.Equals(userLevel, "General", StringComparison.OrdinalIgnoreCase)) return GeneralLevel;
            if (string.Equals(userLevel, "일반 등급", StringComparison.OrdinalIgnoreCase)) return GeneralLevel;
            return string.Empty;
        }

        private static AccountStore LoadStore()
        {
            try
            {
                if (!File.Exists(AccountFilePath)) return new AccountStore();
                AccountStore store = JsonSerializer.Deserialize<AccountStore>(File.ReadAllText(AccountFilePath, Encoding.UTF8));
                if (store == null) store = new AccountStore();
                if (store.Accounts == null) store.Accounts = new List<AccountInfo>();
                return store;
            }
            catch
            {
                return new AccountStore();
            }
        }

        private static void SaveStore(AccountStore store)
        {
            string folderPath = Path.GetDirectoryName(AccountFilePath);
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(AccountFilePath, JsonSerializer.Serialize(store), Encoding.UTF8);
        }
    }

    internal class AccountStore
    {
        public AccountStore()
        {
            Accounts = new List<AccountInfo>();
        }

        public List<AccountInfo> Accounts { get; set; }
    }

    internal class AccountInfo
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserLevel { get; set; }
        public string UserName { get; set; }
        public string CreatedAt { get; set; }
    }
}
