//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventListenertInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public interface ITeamListener {
    void OnTeam(GameEntity entity, int morale, System.Collections.Generic.Dictionary<int, WorkerRole> workers);
}
