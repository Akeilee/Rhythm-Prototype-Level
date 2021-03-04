using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//unused
public class EnemyDeath : MonoBehaviour
{

    public EnemyChange enemy;
    public bool death = false;
    public void EnemyDieYellow(string message) {
        if (message.Equals("FinishDeath")) {
            enemy.deactivateYellow();
            death = true;
        }
        death = false;
    }

    public void EnemyDieGreen(string message) {
        if (message.Equals("FinishDeath")) {
            enemy.deactivateGreen();
            death = true;
        }
        death = false;
    }

    public void EnemyDieRed(string message) {
        if (message.Equals("FinishDeath")) {
            enemy.deactivateRed();
            death = true;
        }
        death = false;
    }

    public void EnemyDieDodo(string message) {
        if (message.Equals("FinishDeath")) {
            enemy.deactivateDodo();
            death = true;
        }
        death = false;
    }

}
