///Purpose:
///Stores Sequence data for Hammer++.
///Yes this is boilerplate as fuck, but it works... i hope.

namespace HammerPP_Manager
{
    internal class MasterSequence
    {
        internal static readonly MasterSequence[] HPPSequences = 
        { 
            new MasterSequence("!Hammer++ Fast LDR", new Sequence[] 
                { 
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-fast -game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-ldr -bounce 2 -noextra -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Fast HDR", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-fast -game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-hdr -bounce 2 -noextra -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Fast LDR + HDR", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-fast -game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-both -bounce 2 -noextra -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Normal LDR", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-ldr -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Normal HDR", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-hdr -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Normal LDR + HDR", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-both -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Final Compile LDR (slow!)", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-ldr -final -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Final Compile HDR (slow!)", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-hdr -final -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ Final Compile LDR + HDR (slow!)", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-both -final -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            ),
            new MasterSequence("!Hammer++ rmod8's Final Compile (Requires Slammin' Source Tools, very slow!)", new Sequence[]
                {
                    new Sequence("$bsp_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$vis_exe", "-game $gamedir $path\\$file"),
                    new Sequence("$light_exe", "-bounce 250 -extrapasses 2 -translucentshadows -worldtextureshadows -ambientocclusion -final -aosamples 16 -softencosine -hdr -game $gamedir $path\\$file"),
                    new Sequence(null, "$path\\$file.bsp $bspdir\\$file.bsp", "257"),
                    new Sequence("$game_exe", "-dev -console -allowdebug -hijack -game $exedir +map $file")
                }
            )
        };

        internal string sequenceName = "\t\"";
        internal Sequence[] sequences;
        internal MasterSequence(string sequenceName, Sequence[] sequences)
        {
            this.sequenceName += sequenceName+="\"";
            this.sequences = sequences;
        }
    }
    internal class Sequence
    {
        internal string enable = "\t\t\t\"enable\"\t\t\"1\"";
        internal string specialcmd = "\t\t\t\"specialcmd\"\t\t\"";
        internal string run = "\t\t\t\"run\"\t\t\"";
        internal string parms = "\t\t\t\"parms\"\t\t\"";
        internal Sequence(string run, string parms, string specialcommand = "0")
        {
            this.run += run +="\"";
            this.parms += parms += "\"";
            this.specialcmd+= specialcommand+="\"";
        }
    }
}
