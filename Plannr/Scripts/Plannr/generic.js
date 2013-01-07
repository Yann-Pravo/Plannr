var DeleteButton = Backbone.View.extend({
    tagName:'a',
    className:'btn btn-danger',
    initialize:function(options) {
        _.bindAll(this,'render','deleteItem')
        if (typeof options.defaultTemplate == 'undefined') this.defaultTemplate = "Supprimer"
        else this.defaultTemplate = options.defaultTemplate
    },
    events :{
        'click' : 'deleteItem'
    },
    render:function() {
        this.$el.html(this.defaultTemplate)
        return this
    },
    deleteItem:function() {
        // Always wait for server acknowledgment
        this.model.destroy({wait:true})
    }
})