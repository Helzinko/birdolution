using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameType : MonoBehaviour
{
    public enum SoundTypes
    {
        music = 0,
        bird_hurt = 1,
        bird_merge = 2,
        box_buy = 3,
        box_open = 4,
        bird_move = 5,
        bird_punch = 6,
        eagle_spawn = 7,
        ui_error = 8,
        place_land = 9,
        game_over = 10,
    }

    public enum ObjectType
    {                                                                                                                       
        trap = 0,
        grass = 1,
    }
}
