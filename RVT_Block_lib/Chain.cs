using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RVT_Block_lib
{
    public class Chain
    {
        
        public List<Block> Blocks { get; set; }
        public Block Last { get;  set; }
        public Chain()
        {
            Blocks = new List<Block>();
            var genesisBlock = new Block();
            Blocks.Add(genesisBlock);
            Last = genesisBlock;
        }



        public void Add(Chooser chooser)
        {
            var block = new Block(chooser, Last);
            Blocks.Add(block);
            Last = block;
        }



        public bool Check()
        {
            var genesisBlock = new Block();
            var prevHash = genesisBlock.Hash;

            foreach(var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if (prevHash != hash)
                {
                    return false;
                }
                prevHash = block.Hash;
            }
            return true;
        }

    }
}
