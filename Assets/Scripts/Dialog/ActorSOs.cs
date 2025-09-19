using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorSO", menuName = "Dialog/Actors")]
public class ActorSO : ScriptableObject
{
    public string actor_name_;
    public Image sprite_; //TODO: attach this to the actual dialog sprite
}
