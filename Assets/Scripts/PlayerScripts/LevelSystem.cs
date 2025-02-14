using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using static SkillTree;
using static PlayerSkillsystem;
using static PlayerAttributes;

public class LevelSystem
{
    public int level;               // Integer to save the current level of the player.
    public float exp;               // Float to save the current experience of the player.
    public float expToLevelUp;      // Float to save the needed experience to level up.
    public int skillpoints;         // Integer to save the current skillpoints of the player.

    /// <summary>
    /// Sets the starting values of each variable.
    /// </summary>
    public LevelSystem() // Setting standard variables to LevelSystem
    {
        level = 1;
        exp = 0;
        skillpoints = 1;
        expToLevelUp = 500;
    }

    /// <summary>
    /// Adds experience to the current levelsystem and increases level and skillpoints, if the experience threshold is reached.
    /// Also plays visual effects for leveling up and updates the skilltree interface.
    /// </summary>
    /// <param name="amount">Sets the amount of experience gained.</param>
    public void AddExp(int amount) // Gain experience and level up
    {
        exp += amount;
        while (exp >= expToLevelUp)
        {
            level++;
            skillpoints++;
            exp -= expToLevelUp;
            if (level % 5 == 0)
            {
                playerAttributesScript.playerAttributes[0].totalAttributValue.TotalAttributeValue += 15;
                playerAttributesScript.playerAttributes[1].totalAttributValue.TotalAttributeValue += 10;
                playerAttributesScript.playerAttributes[2].totalAttributValue.TotalAttributeValue += 5;
                playerAttributesScript.AttributeModified();
            }
            if (level < 11)
            {
                expToLevelUp += expToLevelUp * 0.175f;
            }
            playerskillsystem.PlayLvlUpEffect();
            skillTree.UpdateAllSkillUI();
        }
    }

    /// <summary>
    /// Returns the current level of the player, to call in other scripts.
    /// </summary>
    /// <returns>The current level of the player.</returns>
    public int GetLevel() // Return current level
    {
        return level;
    }
    
    /// <summary>
    /// Returns the current skillpoints of the player, to call in other scripts.
    /// </summary>
    /// <returns>The current skillpoints of the player.</returns>
    public int GetSp() // Return current skillpoints
    {
        return skillpoints;
    }

    /// <summary>
    /// Returns the current experience of the player needed to level up, to call in other scripts.
    /// </summary>
    /// <returns>The current experience of the player needed to level up.</returns>
    public float GetExpToLevelUp() // Return currently needed expierence in order to level up
    {
        return expToLevelUp;
    }
    
    /// <summary>
    /// Returns the current experience of the player, to call in other scripts.
    /// </summary>
    /// <returns>The current experience of the player.</returns>
    public float GetExp() // Return currently needed expierence in order to level up
   {
        return exp;
    }

}
