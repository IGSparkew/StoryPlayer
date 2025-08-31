def execute(api,args):
    if api.hasFlag("king") and api.getFlag("king"):
        api.setFlag("#go_village", True)
        return "The king is here you can't enter"
    api.setFlag("king", True)
    return "You are enter in the castle the king is not here"