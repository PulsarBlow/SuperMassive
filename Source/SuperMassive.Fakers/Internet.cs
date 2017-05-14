namespace SuperMassive.Fakers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SuperMassive.Extensions;

    public static class Internet
    {
        static readonly string[] BYTE = 0.To(255).Select(x => x.ToString()).ToArray();
        static readonly string[] HOSTS = new[] { "gmail.com", "yahoo.com", "hotmail.com" };
        static readonly string[] DISPOSABLE_HOSTS = new[] { "mailinator.com", "suremail.info", "spamherelots.com", "binkmail.com", "safetymail.info", "tempinbox.com" };
        static readonly string[] DOMAIN_SUFFIXES = new[] { "co.uk", "com", "us", "uk", "ca", "biz", "info", "name" };
        static readonly string[] IMAGE_SUFFIXES = new[] { "jpg", "png", "svg", "gif" };

        public static string Email(string name = null)
        {
            return UserName(name) + '@' + DomainName();
        }

        /// <summary>
        /// Returns an email address of an online disposable email service (like tempinbox.com).
        /// you can really send an email to these addresses an access it by going to the service web pages.
        /// <param name="name">User Name initial value.</param>
        /// </summary>
        public static string DisposableEmail(string name = null)
        {
            return UserName(name) + '@' + DISPOSABLE_HOSTS.RandPick();
        }

        public static string FreeEmail(string name = null)
        {
            return UserName(name) + "@" + HOSTS.RandPick();
        }

        /// <summary>
        /// Generates a random user name.
        /// You can pass some space separated names as bootstrap for random pick.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string UserName(string name = null)
        {
            if (name != null)
            {
                string parts = name.Split(' ').Join(new[] { ".", "_" }.RandPick());
                return parts.ToLower();
            }
            else
            {
                switch (RandomNumberGenerator.Int(2))
                {
                    case 0:
                        return new Regex(@"\W").Replace(Name.FirstName(), "").ToLower();
                    case 1:
                        var parts = new[] { Name.FirstName(), Name.LastName() }.Select(n => new Regex(@"\W").Replace(n, ""));
                        return parts.Join(new[] { ".", "_" }.RandPick()).ToLower();
                    default: throw new ApplicationException();
                }
            }
        }

        public static string DomainName()
        {
            return DomainWord() + "." + DomainSuffix();
        }

        public static string DomainWord()
        {
            string dw = Company.Name().Split(' ').First();
            dw = new Regex(@"\W").Replace(dw, "");
            dw = dw.ToLower();
            return dw;
        }

        public static string DomainSuffix()
        {
            return DOMAIN_SUFFIXES.RandPick();
        }

        public static string Uri(string protocol)
        {
            return protocol + "://" + DomainName();
        }

        public static string HttpUrl()
        {
            return Uri("http");
        }
        public static string ImageUrl()
        {
            return String.Format("http://{0}/{1}", DomainName(), Guid.NewGuid().ToString());
        }
        public static string IP_V4_Address()
        {
            return BYTE.RangeRandPick(4).Join(".");
        }
    }
}
