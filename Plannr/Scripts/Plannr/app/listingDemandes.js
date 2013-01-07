

var Demande = Backbone.Model.extend({
    idAttribute:'Id',
    urlRoot:'/api/Book',

    initialize:function() {
        _.bindAll(this,'updateDateFormat')
        this.updateDateFormat();
        this.on('change', this.updateDateFormat, this);
    },

    updateDateFormat:function() {
        if (this.has('DateVoulue')) {
            var newDate = new Date(this.get('DateVoulueTimestamp')*1000)
            
            var printDate = newDate.getDate() + "/" + parseInt(parseInt(newDate.getMonth())+1) + "/" + newDate.getFullYear();
            
            this.set('DateVoulue', printDate)
        }
        if (this.has('DateDemande')) {
            var newDate = new Date(this.get('DateDemandeTimestamp') * 1000)

            var printDate = newDate.getDate() + "/" + parseInt(parseInt(newDate.getMonth())+1) + "/" + newDate.getFullYear();

            this.set('DateDemande', printDate)
        }


        //if (this.has('DateDemande')) this.set('DateDemande',new Date(this.get('DateDemandeTimestamp')).format("dd/mm/yy"))
    }
});

var Demandes = Backbone.Collection.extend({
    model:Demande,
    initialize:function () {


    }
})


var Listing = Backbone.View.extend({
    initialize:function (options) {

        _.bindAll(this, 'render')
        // Keep track of original collection, in case we want to reset filters or so
        this.originalCollection = this.collection

        // Set element to default or new
        if (typeof options.el == 'undefined') this.setElement($('#listing'))
        else this.setElement($(options.el))


        // Bind
        this.collection.on('reset', this.render)

    },
    render:function () {
        // Always clear innerHTML on render
        this.$el.html('')
        var that = this

        this.collection.each(function (model) {

            var subView = new ListingItem({model:model})
            that.$el.append(subView.render().$el)
        })

        return this
    }

})


var ListingItem = Backbone.View.extend({
    tagName:'tr',
    initialize:function () {
        // Template
        _.bindAll(this, 'render', 'remove')
        this.template = Handlebars.compile("<td>{{DateDemande}}</td><td>{{Enseignement_Libelle}}</td><td>{{DateVoulue}}</td><td>{{CreneauSouhaite_Libelle}}</td><td>{{CapaciteNecessaire}}</td><td><input type=\"checkbox\" disabled {{#BesoinProjecteur}} checked{{/BesoinProjecteur}}/></td><td>{{#if ReservationAssociee_Id}}<span class=\"label label-success\">Réservation OK!</span>{{/if}}{{#unless ReservationAssociee_Id}}<span class=\"label label-info\">Non assigné</span>{{/unless}}<td class=\"deleteButton\"></td>")

        // Listen on model destroy
        this.model.on('destroy', this.remove)

    },
    render:function () {
        this.$el.html(this.template(this.model.toJSON()))
        // Append delete button
        this.$el.find('.deleteButton').html(new DeleteButton({model:this.model, className:'btn btn-small', defaultTemplate:'<i class="icon-remove" style="margin-right:0px;"></i>'}).render().$el)
        return this
    },
    remove:function () {
        var that = this
        this.$el.fadeOut("slow", function () {
            that.$el.remove()
        })
    }
})


// Instanciate this
var demandesCollection = new Demandes()

var listingView = new Listing({collection:demandesCollection});
