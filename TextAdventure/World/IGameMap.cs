﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Entities;
using TextAdventure.Items;

namespace TextAdventure.World
{
    public interface IGameMap : IUpdatable
    {
        IEnumerable<IMapNode> Nodes { get; }

        IMapNode EntryNode { get; }

        IMapNode LocationOf(Entity entity);

        IMapNode LocationOf(Item item);

        IMapNode GetRandomNode(Random random);

        List<Path> GetPathsFrom(IMapNode node);

        List<Path> FindPath(IMapNode from, IMapNode to);

        void MoveEntity(Entity entity, Path path);
    }
}
