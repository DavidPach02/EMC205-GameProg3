using UnityEngine;
using System.Collections.Generic;

public interface IMatchMaker {
    List<IPlayer> CreateMatchup();
}
