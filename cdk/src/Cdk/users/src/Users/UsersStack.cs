using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
using Constructs;
using System;
using System.Security.Cryptography;
using System.Text;

namespace UsersStack
{
    public class UsersStack : Stack
    {
        private const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        public UsersStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Create the FullAccess group
            var fullAccessGroup = new CfnGroup(this, "IAMGroup00FullAccess005esCX", new CfnGroupProps
            {
                GroupName = "FullAccess",
                ManagedPolicyArns = new[]
                {
                    "arn:aws:iam::aws:policy/AdministratorAccess"
                }
            });

            // Create Mark's user
            CreateIamUser("IAMUser00MarkHeddaeus00swrB3", "Mark.Heddaeus", fullAccessGroup, new[]
            {
                "arn:aws:iam::aws:policy/IAMUserChangePassword",
                "arn:aws:iam::aws:policy/AdministratorAccess"
            });

            // Create Rick's user
            CreateIamUser("IAMUser00RickRegganie00EPAxD", "Rick.Regganie", fullAccessGroup, new[]
            {
                "arn:aws:iam::aws:policy/IAMUserChangePassword",
                "arn:aws:iam::aws:policy/AdministratorAccess"
            });

            // Create Diane's user
            CreateIamUser("IAMUser00DianeSiebert00xPVwk", "Diane.Siebert", fullAccessGroup, Array.Empty<string>());

            // Create Scott's user
            CreateIamUser("IAMUser00ScottPittard00Zr5ID", "Scott.Pittard", fullAccessGroup, new[]
            {
                "arn:aws:iam::aws:policy/IAMUserChangePassword"
            });
        }

        private CfnUser CreateIamUser(string id, string username, CfnGroup group, string[] managedPolicyArns)
        {
            string initialPassword = GenerateSecurePassword();

            var user = new CfnUser(this, id, new CfnUserProps
            {
                UserName = username,
                Path = "/",
                ManagedPolicyArns = managedPolicyArns,
                LoginProfile = new CfnUser.LoginProfileProperty
                {
                    Password = initialPassword,
                    PasswordResetRequired = true
                },
                Groups = new[] { group.Ref }
            });

            // Output the initial password
            new CfnOutput(this, $"Password{username.Replace(".", "")}", new CfnOutputProps
            {
                Value = initialPassword,
                Description = $"Initial password for user {username}"
            });

            return user;
        }

        private string GenerateSecurePassword()
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";

            var password = new StringBuilder();
            using (var rng = RandomNumberGenerator.Create())
            {
                // Add at least one of each required character type
                password.Append(GetRandomChar(uppercaseChars, rng));
                password.Append(GetRandomChar(lowercaseChars, rng));
                password.Append(GetRandomChar(numberChars, rng));
                password.Append(GetRandomChar(specialChars, rng));

                // Add additional random characters to meet minimum length
                const string allChars = uppercaseChars + lowercaseChars + numberChars + specialChars;
                while (password.Length < 12) // Using 12 as minimum length for better security
                {
                    password.Append(GetRandomChar(allChars, rng));
                }
            }

            // Shuffle the password characters
            return ShuffleString(password.ToString());
        }

        private char GetRandomChar(string characters, RandomNumberGenerator rng)
        {
            byte[] buffer = new byte[1];
            char result;
            do
            {
                rng.GetBytes(buffer);
                result = characters[buffer[0] % characters.Length];
            } while (!char.IsLetterOrDigit(result) && !specialChars.Contains(result));

            return result;
        }

        private string ShuffleString(string str)
        {
            var array = str.ToCharArray();
            using (var rng = RandomNumberGenerator.Create())
            {
                int n = array.Length;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do
                    {
                        rng.GetBytes(box);
                    } while (!(box[0] < n * (byte.MaxValue / n)));
                    int k = (box[0] % n);
                    n--;
                    char temp = array[n];
                    array[n] = array[k];
                    array[k] = temp;
                }
            }
            return new string(array);
        }
    }
}
