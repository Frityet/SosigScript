--All SosigScripts must have a ScriptInfo descriptor
ScriptInfo = {
    Name        = "Example SosigScript 1",
    GUID        = "net.frityet.sosigscript.example",
    Author      = "Frityet",
    Version     = "1.0.0",
}

--The awake function will be run as soon as this script starts
function awake()
    print("Hello, World!")
end
