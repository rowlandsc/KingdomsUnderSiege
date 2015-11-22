using UnityEngine;
using System.Collections;

/**
 * The interface that all Minion combat scripts should implement
 */
public interface IMinion_Attack {
    IEnumerator Attack(Transform target);
}
