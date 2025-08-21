def execute(api, args):
    if api.hasFlag("has_item"):
        return "You already have the item in your inventory."
    item = args["item"]
    api.addItem(item)
    api.setFlag("has_item", True)
    return "Item " + item + " added successfully to inventory."
