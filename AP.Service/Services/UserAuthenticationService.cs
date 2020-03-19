using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AP.Core.Dtos;
using AP.Core.Services;
using AP.Persistence.Contexts;
using System.Linq;


namespace AP.Service
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly APContext _context;


        public UserAuthenticationService(APContext context)
        {
            _context = context;
        }

        public async Task<bool> IsLoginAsync(string hash)
        {
            var tuple = Decode(hash);
            var result = await _context.Users.AsNoTracking()
                                             .Where(u => u.Username == tuple.Item1 && u.Password == tuple.Item2)
                                             .FirstOrDefaultAsync();
            if(result == null) return false;
            
            return true;                                
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var result = await _context.Users.AsNoTracking()
                                             .Where(u => u.Username == username && u.Password == password)
                                             .FirstOrDefaultAsync();
            if(result == null) return null;
            
            return Encode(username,password);
        }

        private string Encode(string username,string password)
        {
            long @base = (long)Math.Pow(10,9) + 7;
            long root = 221435537643948336;
            long radius = 0;

            foreach(char letter in username)
                radius = (radius*root + (int)letter) % @base; 
            foreach(char letter in password)
                radius = (radius*root + (int)letter) % @base;

            long space =(int)( radius % (username.Length + password.Length) == 0 ? Math.Abs(radius % (username.Length + password.Length + 1)) : Math.Abs(radius % (username.Length + password.Length))); 
            List<char> work = new List<char>();
            work.Add((char)space);
            work.Add((char)username.Length);
            work.Add((char)password.Length);
            foreach(char letter in username)
            {
                work.Add(letter);
                for (int i = 0; i < space; i++)
                {
                    long value = (radius + root*i)/(int)letter % 122 < 65 ? (radius + root*i)/(int)letter % 122 + 65 :(radius + root*i)/(int)letter % 122; 
                    work.Add((char)(int)value);
                }
            }
                        
            foreach(char letter in password)
            {
                work.Add(letter);
                for (int i = 0; i < space; i++)
                {
                    int value = (int)Math.Abs((radius*i*(int)letter) % 122);
                    if (value < 65) value += 65;
                    if (value > 122) value -= 65;
                    work.Add((char)value);
                }
            }
            return new String(work.ToArray());
        }
        private Tuple<string,string> Decode(string hash)
        {
            int space = (int)hash[0];
            int user_lengh = (int)hash[1];
            int pass_lengh = (int)hash[2];
            char[] username = new char[user_lengh];
            char[] password = new char[pass_lengh];
            int i = 0,j = 3;
            for (; i < user_lengh; i++,j+= space + 1)
            {
                username[i] = hash[j];
            }
            for (i = 0; i < pass_lengh; i++,j+= space + 1)
            {
                password[i] = hash[j];
            }

            return new Tuple<string, string>(new String(username),new String(password));
        }

    }
}
