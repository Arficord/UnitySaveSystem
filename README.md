# UnitySaveSystem
Save system asset for Unity. Has a demo scene inside. Currently, it is in an early development state.
The system has module structure and divided into parts:

## Seializer
ScriptableObject. Serialize data from objects.
Currently, there are 2 serializers: JSON and Binary.

## Store
ScriptableObject. Store serialized data.
Currently there are 2 stores: Disca and PlayerPrefs.

## Saver
MonoBehaviour. Gather and apply data to object.
For example: PlayerSaver gathers and applies data to a player.

## Other

Modules for each part can be freely added.
