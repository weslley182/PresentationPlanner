guidKey = ''
function deleteContact(guid) {
    guidkey = guid
    confirm('Are you sure?', toDelete)
}

function toDelete() {
    $.ajax("/api/Planner/" + guidkey, { method: "delete" })
        .then(function (response) {
            location.reload()
        })
}