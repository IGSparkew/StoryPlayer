def execute(api,args):
    if api.hasFlag("hurt") and api.getFlag("hurt"):
        api.setFlag("shop", True)
        return "You enter in a shop and hide from zombies"
    api.setFlag("hurt", True)
    return "You are entering the town, and you are hurt by zombies"