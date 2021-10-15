using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vann
{
    class Engine
    {

        public static List<Token> last_line_tokens = new List<Token>();

        public static IDictionary<string, string> vars = new Dictionary<string, string>();


        
        public static void AddVarOrOverride(string name,string value)
        {
            if(vars.ContainsKey(name))
            {
                vars[name] = value;
            }
            else
            {
                vars.Add(name, value);
            }
        }

        public static string GetValueOfVarByName(string name)
        {
            if(vars[name] != null)
            {
                return vars[name];
            }
            else
            {
                SetError("Can't find that variable : " + name);
                DisplayCurrentError();
            }


            return "";
        }

        public static void RunLastLineTokens()
        {
            
            int i = 0;
            foreach(Token x in last_line_tokens)
            {
                if(i == 0)
                {
                    if(x.value == "log")
                    {
                        //log(merhaba dünya)
                        try
                        {
                            if (last_line_tokens[i + 1].type == Globals.TYPES.PARAN_START)
                            {
                                int j = 0;
                                int end_at = 0; 
                                while(true)
                                {
                                    

                                    if(last_line_tokens[j].type == Globals.TYPES.PARAN_END)
                                    {
                                        //ŞARTLAR TAMMM
                                        end_at = j;
                                        break;
                                    }
                                    else
                                    {
                                        if (j >= last_line_tokens.Count)
                                        {
                                            SetError("Unexpected Syntax For ')'");
                                            DisplayCurrentError();
                                           
                                            return;
                                        }
                                        else
                                        {
                                            j++;
                                        }
                                            
                                    }

                                }
                                string to_write = "";
                                
                                for (int k = 2;k <= end_at-1; k++)
                                {
                                    
                                    if(last_line_tokens[k].type == Globals.TYPES.PARAN_SUS_START)
                                    {
                                        int start_in = k+1;
                                        int end_in = 0;
                                        int ja = start_in;

                                        while (true)
                                        {


                                            if (last_line_tokens[ja].type == Globals.TYPES.PARAN_SUS_END)
                                            {
                                               
                                                end_in = ja;
                                                break;
                                            }
                                            else
                                            {
                                                if (j >= last_line_tokens.Count)
                                                {
                                                    SetError("Unexpected Syntax For '}' while running 'var'");
                                                    DisplayCurrentError();

                                                    return;
                                                }
                                                else
                                                {
                                                    ja++;
                                                }

                                            }
                                         

                                        }
                                        string to_value = "";
                                        for (int ka = start_in; ka <= end_in - 1; ka++)
                                        {
                                           
                                            to_value += last_line_tokens[ka].value;
                                            last_line_tokens[ka].value = "";
                                            
                                            

                                            
                                        }

                                        if(GetValueOfVarByName(to_value) == null)
                                        {
                                            SetError("Can't find this variable : " + to_value);
                                            DisplayCurrentError();
                                            return;
                                        }

                                        string degisken_val = GetValueOfVarByName(to_value);
                                        
                                       

                                        to_write += degisken_val;
                                     
                                    }
                                    else if((last_line_tokens[k].type == Globals.TYPES.PARAN_SUS_END))
                                    {

                                        continue;
                                    }
                                    else
                                    {

                                        to_write += last_line_tokens[k].value + " ";
                                    }
                                
                              
                                }

                                Console.WriteLine(to_write);
                     

                            }
                            else
                            {
                                SetError("Unexpected Syntax For '('");
                                DisplayCurrentError();
                                return;
                            }
                        }
                        catch(Exception ex)
                        {
                            SetError($"Unexpected Syntax For ')' while running 'log' " + ex.Message);
                            DisplayCurrentError();
                            return;
                        }
                
                    
                               
                     
                    }
                    else if(x.value == "var")
                    {
                        try
                        {
                       
                            //var name (kaan temizkan)

                            string var_name = last_line_tokens[i + 1].value;
                            int start_index;
                            int end_index;
                            if(last_line_tokens[i+2].type == Globals.TYPES.PARAN_START)
                            {
                                start_index = 3;
                            }
                            else
                            {
                                SetError("Unexpected Syntax For '(' while running 'var'");
                                DisplayCurrentError();
                                return;
                            }

                            int j = 0;
                       
                            while (true)
                            {


                                if (last_line_tokens[j].type == Globals.TYPES.PARAN_END)
                                {
                                    //ŞARTLAR tamamammmke
                                    end_index = j;
                                    break;
                                }
                                else
                                {
                                    if (j >= last_line_tokens.Count)
                                    {
                                        SetError("Unexpected Syntax For ')' while running 'var'");
                                        DisplayCurrentError();

                                        return;
                                    }
                                    else
                                    {
                                        j++;
                                    }

                                }

                            }
                            string to_write = "";
                            for (int k = start_index; k <= end_index - 1; k++)
                            {
                                if(k == end_index-1)
                                {
                                    to_write += last_line_tokens[k].value;

                                }
                                else
                                {
                                    to_write += last_line_tokens[k].value + " ";
                                }
                                
                            }

                            


                            AddVarOrOverride(var_name, to_write);

                         


                        }
                        catch
                        {
                            SetError("An error occured while running 'var'");
                            DisplayCurrentError();
                        }
                    }
                    else if(x.value == "inp")
                    {
                        string degisken_name = last_line_tokens[i + 1].value;

                        string degisken_value = Console.ReadLine();

                        AddVarOrOverride(degisken_name, degisken_value);


                    }
                    else if(x.type == Globals.TYPES.COMMENT)
                    {
                        break;
                    }
              
                    
                    
                }
                i++;
            }
            
    
        }

        public static void TokenizeString(string query)
        {
            last_line_tokens.Clear();
            query = query.Trim(' ');
            query = query.Replace("(", " ( ");
            query = query.Replace(")"," ) ");
            query = query.Replace("'"," ' ");
            query = query.Replace("{", " { ");
            query = query.Replace("}", " } ");
            query = query.Replace("=>", " => ");
            query = query.Replace("//", " // ");

            string[] s_query = query.Split(' ');
            

            foreach(string kelime in s_query)
            {
                if (kelime == "(") { last_line_tokens.Add(new Token(Globals.TYPES.PARAN_START, "(")); }
                else if (kelime == ")") { last_line_tokens.Add(new Token(Globals.TYPES.PARAN_END, ")")); }
                else if (kelime == "'") { last_line_tokens.Add(new Token(Globals.TYPES.QUETE, "'")); }
                else if (kelime == "{") { last_line_tokens.Add(new Token(Globals.TYPES.PARAN_SUS_START, "{")); }
                else if (kelime == "}") { last_line_tokens.Add(new Token(Globals.TYPES.PARAN_SUS_END, "}")); }
                else if (kelime == "log") { last_line_tokens.Add(new Token(Globals.TYPES.COMMAND, kelime)); }
                else if (kelime == "//") { last_line_tokens.Add(new Token(Globals.TYPES.COMMENT, kelime)); }
                else if (kelime == "") { continue; }
                else { last_line_tokens.Add(new Token(Globals.TYPES.STRING, kelime)); }
   

         
            }

        }


        public static void SetError(string value)
        {
            
            Globals.errorValue = $"[{DateTime.Now.ToLongTimeString()}] => {value}";
        }

        public static void DisplayCurrentError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error : {Globals.errorValue}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
