//Current Parameters
var report = 'null';

var company = 'null';
var division = 'null';
var name = 'null';
var badge = 'null';
var wildCardId = 'null';
var wildCardText = 'null';

var facility = 'null';
var area = 'null';
var category = 'null';
var reader = 'null';

var stDate = 'null';
var endDate = 'null';
var stTime = 'null';
var endTime = 'null';

var misc1 = 'null';
var misc2 = 'null';

// Parameter IDs

var _report = 'null';

var _company = 'null';
var _division = 'null';
var _name = 'null';
var _badge = 'null';
var _wildCardId = 'null';
var _wildCardText = 'null';

var _facility = 'null';
var _area = 'null';
var _category = 'null';
var _reader = 'null';

var _stDate = 'null';
var _endDate = 'null';
var _stTime = 'null';
var _endTime = 'null';

var _misc1 = 'null';
var _misc2 = 'null';

// Bloodhound Engines

var report_;

var company_;
var division_;
var name_;
var badge_;
var wildCardId_;
var wildCardText_;

var facility_;
var area_;
var category_;
var reader_;

var misc1_;
var misc2_;

function bloodhoundInit(initialApiName, apiName) {
    bloodhound = new Bloodhound({
        datumTokenizer: function(d) {
            return Bloodhound.tokenizers.whitespace(d.value);
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        prefetch: {
            url: ROOTURL + initialApiName,
            filter: function filter(data) {
                return $.map(data.result, function(datum) {
                    return {
                        value: datum.value,
                        data: datum.key
                    };
                });
            }
        },

        limit: 10,

        remote: {
            url: ROOTURL + apiName + '/%QUERY',
            filter: filter,
            replace: function(url, query) {
                var q = ROOTURL + apiName + '/' + query;
                return q;
            }
        },
        dupDetector: function(remoteMatch, localMatch) {
            return remoteMatch.value === localMatch.value;
        }
    });

    function filter(data) {
        var map = $.map(data.result, function(datum) {
            return {
                value: datum.value,
                data: datum.key
            };
        });
        return map;
    }

    return bloodhound;
}

function typeaheadInit(id, bloodhound) {

    bloodhound.initialize(true);

    $('#' + id).typeahead({
        hint: true,
        minLength: 0
    }, {
        name: id,
        displayKey: 'value',
        source: bloodhound.ttAdapter()
    }).bind('typeahead:selected', function(obj, datum) {
        selectHandler(id, obj, datum);
        $(this)[tog(this.value)]('x');
    });

    enableParam(id);
}

function noParamSelected() {
    if (company == 'null' && 
        division == 'null' &&
        name == 'null' &&
        badge == 'null' &&
        wildCardText == 'null' &&
        facility == 'null' &&
        area == 'null' &&
        category == 'null' &&
        reader == 'null' &&
        stDate == 'null' &&
        stTime == 'null' &&
        endDate == 'null' &&
        endTime == 'null' &&
        misc1 == 'null' &&
        misc2 == 'null') {
        return true;
    }
    return false;
}

function tog(v) {
    return v ? 'addClass' : 'removeClass';
}

$(document).on('input', '.clearable', function() {
    $(this)[tog(this.value)]('x');
}).on('mousemove', '.x', function(e) {
    $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
}).on('click', '.onX', function() {
    $(this).removeClass('x onX').val('');
    $(this).typeahead('val', '');
    deselectHandler(this.id);
});

function clearVal_(id) {
    $('#' + id).removeClass('x onX').val('');
    $('#' + id).typeahead('val', '');
    deselectHandler(id);
}

function clear_(bloodhound, id) {
    if (bloodhound) {
        bloodhound.clearPrefetchCache();
        bloodhound.clearRemoteCache();
        bloodhound.clear();
    }

    $('#' + id).removeClass('x onX').val('');
    switch (id) {
        case 'report':
            report = 'null';
            _report = 'null';
            break;
        case 'company':
            company = 'null';
            _company = 'null';
            break;
        case 'division':
            division = 'null';
            _division = 'null';
            break;
        case 'name':
            name = 'null';
            _name = 'null';
            break;
        case 'badge':
            badge = 'null';
            _badge = 'null';
            break;
        case 'wildCardId':
            wildCardId = 'null';
            _wildCardId = 'null';
            break;
        case 'wildCardText':
            wildCardText = 'null';
            _wildCardText = 'null';
            break;
        case 'facility':
            facility = 'null';
            _facility = 'null';
            break;
        case 'area':
            area = 'null';
            _area = 'null';
            break;
        case 'category':
            category = 'null';
            _category = 'null';
            break;
        case 'reader':
            reader = 'null';
            _reader = 'null';
            break;
        case 'misc1':
            misc1 = 'null';
            _misc1 = 'null';
            break;
        case 'misc2':
            misc2 = 'null';
            _misc2 = 'null';
            break;
    }
}

function disableParam(id) {
    $('#' + id).prop('disabled', true);
    $('#' + id).css('background-color', 'rgb(235, 235, 228);');
}

function enableParam(id) {
    $('#' + id).prop('disabled', false);
    $('#' + id).css('background-color', 'rgb(255, 255, 255);');
}

//Cloning
var boxCounter = 1;
    
$(document).ready(function () {

    $(document).on("click", ".addCb", function Add() {

        if (facility != 'null' || area != 'null' || category != 'null' || reader != 'null') {
            var label;
            var id;

            if (facility != 'null') {
                lbl = 'Facility: ' + facility;
                id = _facility + ',' + 'null,null,null' ;
            }

            if (area != 'null') {
                lbl = 'Area: ' + area;
                id = 'null,' + _area + ',null,null';
            }
            if (category != 'null') {
                lbl = 'Category: ' + category;
                id = 'null,null,' + _category + ',null';
            }
            if (reader != 'null') {
                lbl = 'Reader: ' + reader;
                id = 'null,null,null,' +  reader;
            }

            $("#cloneTable").append( 
                "<tr>" + 
                "<td><img src='resources/images/unchecked.gif' class='btnDelete'/></td>" + 
                "<td class='cloneDatum'><span name = 'cloneItem-" + boxCounter + "' id = '" + id + "' >" + lbl +"</td>" + 
                "</tr>");

            boxCounter++;
        }
    });

    $(document).on("click", ".btnDelete", function Delete() {
        $(this).closest('tr').remove();
    });

});

//End Cloning

function deselectHandler(id) {

    switch (id) {
        case 'company':

            company = 'null';
            _company = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            //clear_(company_, 'company');
            clear_(division_, 'division');
            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');

            //$('#company').typeahead('destroy');
            $('#division').typeahead('destroy');
            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');

            //company_ = bloodhoundInit('Company', 'Company');
            division_ = bloodhoundInit('Division', 'Division/null');
            name_ = bloodhoundInit('Name', 'Name/null/null');
            badge_ = bloodhoundInit('Badge', 'Badge/null/null');
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            //typeaheadInit('company', company_);
            typeaheadInit('division', division_);
            typeaheadInit('name', name_);
            typeaheadInit('badge', badge_);
            typeaheadInit('wildCardId', wildCardId_);

            break;

        case 'division':

            division = 'null';
            _division = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            //clear_(division_, 'division');
            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');

            //$('#division').typeahead('destroy');
            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');

            //division_ = bloodhoundInit('Division/' + _company, 'Division/' + _company);
            name_ = bloodhoundInit('Name/' + _company, 'Name/' + _company);
            badge_ = bloodhoundInit('Badge/' + _company, 'Badge/' + _company);
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            //typeaheadInit('division', division_);
            typeaheadInit('name', name_);
            typeaheadInit('badge', badge_);
            typeaheadInit('wildCardId', wildCardId_);

            break;

        case 'name':
            name = 'null';
            _name = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }


            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');

            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');

            badge_ = bloodhoundInit('Badge/' + _company + '/' + _division, 'Badge/' + _company + '/' + _division);
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            typeaheadInit('badge', badge_);
            typeaheadInit('wildCardId', wildCardId_);

            enableParam('badge');
            enableParam('wildCardId');

            break;

        case 'badge':
            badge = 'null';
            _badge = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            clear_(name_, 'name');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');

            $('#name').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');

            name_ = bloodhoundInit('Name/' + _company + '/' + _division, 'Name/' + _company + '/' + _division);
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            typeaheadInit('name', name_);
            typeaheadInit('wildCardId', wildCardId_);

            enableParam('name');
            enableParam('wildCardId');

            break;

        case 'wildCardId':

            wildCardId = 'null';
            _wildCardId = 'null';

            //clear_(wildCardId_, 'wildCardId');
            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardText_, 'wildCardText');

            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');
            
            badge_ = bloodhoundInit('Badge/' + _company + '/' + _division, 'Badge/' + _company + '/' + _division);
            name_ = bloodhoundInit('Name/' + _company + '/' + _division, 'Name/' + _company + '/' + _division);

            typeaheadInit('name', name_);
            typeaheadInit('badge', badge_);

            enableParam('name');
            enableParam('badge');
            disableParam('wildCardText');

            //wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            //typeaheadInit('wildCardId', wildCardId_);

            enableParam('wildCardId');

            break;

        case 'wildCardText':
            wildCardText = 'null';
            _wildCardText = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }


            break;

        case 'facility':

            facility = 'null';
            _facility = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            //clear_(facility_, 'facility');
            clear_(area_, 'area');
            clear_(category_, 'category');
            clear_(reader_, 'reader');

            //$('#facility').typeahead('destroy');
            $('#area').typeahead('destroy');
            $('#category').typeahead('destroy');
            $('#reader').typeahead('destroy');

            enableParam('area');

            //facility_ = bloodhoundInit('Facility', 'Facility');
            area_ = bloodhoundInit('Area', 'Area/null');
            category_ = bloodhoundInit('Category', 'Category/null/null');
            reader_ = bloodhoundInit('Reader', 'Reader/null/null/null');

            //typeaheadInit('facility', facility_);
            typeaheadInit('area', area_);
            typeaheadInit('category', category_);
            typeaheadInit('reader', reader_);

            if(_report == "Report_AlarmStatus") {
                enableParam('facility');
                disableParam('area');
                disableParam('category');
                disableParam('reader');
            }
            else if (_report == "Report_Activity") {
                enableParam('facility');
                enableParam('area');
                disableParam('category');
                enableParam('reader');
            }
            else if (_report == "Report_DoorCategory") {
                disableParam('facility');
                enableParam('area');
                enableParam('category');
                disableParam('reader');
            }

            break;

        case 'area':

            area = 'null';
            _area = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }


            //clear_(area_, 'area');
            clear_(facility_, 'facility');
            clear_(category_, 'category');
            clear_(reader_, 'reader');

            //$('#area').typeahead('destroy');
            $('#facility').typeahead('destroy');
            $('#category').typeahead('destroy');
            $('#reader').typeahead('destroy');

            enableParam('facility');

            facility_ = bloodhoundInit('Facility', 'Facility');
            //area_ = bloodhoundInit('Area', 'Area/null');
            category_ = bloodhoundInit('Category', 'Category/null/null');
            reader_ = bloodhoundInit('Reader', 'Reader/null/null/null');

            typeaheadInit('facility', facility_);
            //typeaheadInit('area', area_);
            typeaheadInit('category', category_);
            typeaheadInit('reader', reader_);

            if(_report == "Report_AlarmStatus") {
                enableParam('facility');
                disableParam('area');
                disableParam('category');
                disableParam('reader');
            }
            else if (_report == "Report_Activity") {
                enableParam('facility');
                enableParam('area');
                disableParam('category');
                enableParam('reader');
            }
            else if (_report == "Report_DoorCategory") {
                disableParam('facility');
                enableParam('area');
                enableParam('category');
                disableParam('reader');
            }

            break;

        case 'category':
            category = 'null';
            _category = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }


            break;

        case 'reader':
            reader = 'null';
            _reader = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            break;

        case 'misc1':
            misc1 = 'null';
            _misc1 = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            break;

        case 'misc2':
            misc2 = 'null';
            _misc2 = 'null';

            if (noParamSelected()) {
                $('.viewR').removeClass('viewRep').addClass('viewRep-disabled');
                $('.viewR').prop('src', 'resources/images/Go_Icon_disabled.png');
            }

            break;

        case 'report':

            $('#cloneDiv').html('');

            report = 'null';
            _report = 'null';

            clear_(company_, 'company');
            clear_(division_, 'division');
            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');
            clear_(facility_, 'facility');
            clear_(area_, 'area');
            clear_(category_, 'category');
            clear_(reader_, 'reader');
            clear_(misc1_, 'misc1');
            clear_(misc2_, 'misc2');

            $('#misc1txt').text('Disabled');
            $('#misc2txt').text('Disabled');

            $('#misc1txt').css('color', 'rgb(255,255,255)');
            $('#misc2txt').css('color', 'rgb(255,255,255)');

            $('#fromdate').val('');
            $('#fromtime').val('');
            $('#todate').val('');
            $('#totime').val('');

            $('#company').typeahead('destroy');
            $('#division').typeahead('destroy');
            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');

            $('#addClone').prop('src', 'resources/images/button_plus_blank.png');

            $('#facility').typeahead('destroy');
            $('#area').typeahead('destroy');
            $('#category').typeahead('destroy');
            $('#reader').typeahead('destroy');
            $('#misc1').typeahead('destroy');
            $('#misc2').typeahead('destroy');

            disableParam('company');
            disableParam('division');
            disableParam('name');
            disableParam('badge');
            disableParam('wildCardId');
            disableParam('wildCardText');
            disableParam('facility');
            disableParam('category');
            disableParam('area');
            disableParam('reader');
            disableParam('misc1');
            disableParam('misc2');

            $('#fromdate').prop('disabled', true);
            $('#fromtime').prop('disabled', true);
            $('#todate').prop('disabled', true);
            $('#totime').prop('disabled', true);
            $('.day').prop('disabled', true);
            $('.month').prop('disabled', true);


            break;

    }
}

// On typeahead:selected

function selectHandler(id, obj, datum) {

    switch (id) {
        case 'company':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');


            company = datum.value;
            _company = datum.data;

            clear_(division_, 'division');
            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');

            $('#division').typeahead('destroy');
            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');


            division_ = bloodhoundInit('Division/' + _company, 'Division/' + _company);
            name_ = bloodhoundInit('Name/' + _company + '/null', 'Name/' + _company + '/null');

            badge_ = bloodhoundInit('Badge/' + _company + '/null', 'Badge/' + _company + '/null');
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            typeaheadInit('division', division_);
            typeaheadInit('name', name_);
            typeaheadInit('badge', badge_);
            typeaheadInit('wildCardId', wildCardId_);

            break;

        case 'division':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            division = datum.value;
            _division = datum.data;

            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');

            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');

            name_ = bloodhoundInit('Name/' + _company + '/' + _division, 'Name/' + _company + '/' + _division);
            badge_ = bloodhoundInit('Badge/' + _company + '/' + _division, 'Badge/' + _company + '/' + _division);
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            typeaheadInit('name', name_);
            typeaheadInit('badge', badge_);
            typeaheadInit('wildCardId', wildCardId_);

            break;

        case 'name':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            name = datum.value;
            _name = datum.data;

            disableParam('badge');
            disableParam('wildCardId');
            disableParam('wildCardText');

            break;

        case 'badge':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            badge = datum.value;
            _badge = datum.data;

            disableParam('name');
            disableParam('wildCardId');
            disableParam('wildCardText');

            break;

        case 'wildCardId':
            wildCardId = datum.value;
            _wildCardId = datum.data;

            clear_(wildCardText_, 'wildCardText');

            $('#wildCardText').typeahead('destroy');

            wildCardText_ = bloodhoundInit('WildCardData/' + _wildCardId + '/' + _company + '/' + _division, 'WildCardData/' + _wildCardId + '/' + _company + '/' + _division);

            typeaheadInit('wildCardText', wildCardText_);

            disableParam('name');
            disableParam('badge');
            enableParam('wildCardText');

            break;

        case 'wildCardText':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            wildCardText = datum.value;
            _wildCardText = datum.data;

            break;

        case 'facility':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            facility = datum.value;
            _facility = datum.data;

            clear_(area_, 'area');
            //clear_(category_, 'category');
            clear_(reader_, 'reader');

            $('#area').typeahead('destroy');
            //$('#category').typeahead('destroy');
            $('#reader').typeahead('destroy');

            //area_ = bloodhoundInit('Area/' + _facility, 'Area/' + _facility);
            //category_ = bloodhoundInit('Category/' + _facility + '/null', 'Category/' + _facility + '/null');
            reader_ = bloodhoundInit('Reader/' + _facility + '/null/null', 'Reader/' + _facility + '/null/null');

            //typeaheadInit('category', category_);
            //typeaheadInit('area', area_);
            typeaheadInit('reader', reader_);

            if(_report == "Report_AlarmStatus") {
                enableParam('facility');
                disableParam('area');
                disableParam('category');
                disableParam('reader');
            }
            else if (_report == "Report_Activity") {
                enableParam('facility');
                disableParam('area');
                disableParam('category');
                enableParam('reader');
            }
            else if (_report == "Report_DoorCategory") {
                disableParam('facility');
                disableParam('area');
                enableParam('category');
                disableParam('reader');
            }

            break;

        case 'area':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            area = datum.value;
            _area = datum.data;

            clear_(category_, 'category');
            clear_(reader_, 'reader');

            $('#category').typeahead('destroy');
            $('#reader').typeahead('destroy');

            category_ = bloodhoundInit('Category/' + _facility + '/' + _area, 'Category/' + _facility + '/' + _area);
            reader_ = bloodhoundInit('Reader/' + _facility + '/' + _area, 'Reader/' + _facility + '/' + _area);

            typeaheadInit('category', category_);
            typeaheadInit('reader', reader_);

           
            if(_report == "Report_AlarmStatus") {
                disableParam('facility');
                disableParam('area');
                disableParam('category');
                disableParam('reader');
            }
            else if (_report == "Report_Activity") {
                disableParam('facility');
                enableParam('area');
                disableParam('category');
                enableParam('reader');
            }
            else if (_report == "Report_DoorCategory") {
                disableParam('facility');
                enableParam('area');
                enableParam('category');
                disableParam('reader');
            }

            break;

        case 'category':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            category = datum.value;
            _category = datum.data;

            break;

        case 'reader':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            reader = datum.value;
            _reader = datum.data;

            break;

        case 'misc1':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            misc1 = datum.value;
            _misc1 = datum.data;

            break;

        case 'misc2':

            $('.viewR').removeClass('viewRep-disabled').addClass('viewRep');
            $('.viewR').prop('src', 'resources/images/Go_Icon.png');
            $('.viewR').prop('onclick', 'true');

            misc2 = datum.value;
            _misc2 = datum.data;

            break;

        case 'report':

            report = datum.value;
            _report = datum.data;

            clear_(company_, 'company');
            clear_(division_, 'division');
            clear_(name_, 'name');
            clear_(badge_, 'badge');
            clear_(wildCardId_, 'wildCardId');
            clear_(wildCardText_, 'wildCardText');
            clear_(facility_, 'facility');
            clear_(area_, 'area');
            clear_(category_, 'category');
            clear_(reader_, 'reader');
            clear_(misc1_, 'misc1');
            clear_(misc2_, 'misc2');

            $('#fromdate').val('');
            $('#fromtime').val('');
            $('#todate').val('');
            $('#totime').val('');

            $('#company').typeahead('destroy');
            $('#division').typeahead('destroy');
            $('#name').typeahead('destroy');
            $('#badge').typeahead('destroy');
            $('#wildCardId').typeahead('destroy');
            $('#wildCardText').typeahead('destroy');
            $('#facility').typeahead('destroy');
            $('#area').typeahead('destroy');
            $('#category').typeahead('destroy');
            $('#reader').typeahead('destroy');
            $('#misc1').typeahead('destroy');
            $('#misc2').typeahead('destroy');

            company_ = bloodhoundInit('Company', 'Company');
            division_ = bloodhoundInit('Division', 'Division/null');
            name_ = bloodhoundInit('Name', 'Name/null/null');
            badge_ = bloodhoundInit('Badge', 'Badge/null/null');
            wildCardId_ = bloodhoundInit('WildCard', 'WildCard');

            facility_ = bloodhoundInit('Facility', 'Facility');
            area_ = bloodhoundInit('Area', 'Area/null');
            category_ = bloodhoundInit('Category', 'Category/null/null');
            reader_ = bloodhoundInit('Reader', 'Reader/null/null/null');

            typeaheadInit('company', company_);
            typeaheadInit('division', division_);
            typeaheadInit('name', name_);
            typeaheadInit('badge', badge_);
            typeaheadInit('wildCardId', wildCardId_);

            typeaheadInit('facility', facility_);
            typeaheadInit('area', area_);
            typeaheadInit('category', category_);
            typeaheadInit('reader', reader_);


            if (_report == 'Report_Activity') {

                enableParam('company');
                enableParam('division');
                enableParam('name');
                enableParam('badge');
                enableParam('wildCardId');
                disableParam('wildCardText');
                enableParam('facility');
                disableParam('category');
                enableParam('area');
                enableParam('reader');



                misc1_ = bloodhoundInit('Status/1', 'Status/1');
                typeaheadInit('misc1', misc1_);

                enableParam('misc1');
                disableParam('misc2');

                $('#addClone').removeClass('addCb-disabled').addClass('addCb');
                $('#addClone').prop('src', 'resources/images/button_plus.png');

                $('#misc1txt').text('Status');
                $('#misc2txt').text('Disabled');

                $('#misc1txt').css('color', 'rgb(0,0,0)');
                $('#misc2txt').css('color', 'rgb(255,255,255)');

                $('#fromdate').prop('disabled', false);
                $('#fromtime').prop('disabled', false);
                $('#todate').prop('disabled', false);
                $('#totime').prop('disabled', false);
                $('.day').prop('disabled', false);
                $('.month').prop('disabled', false);
            } else if (_report == 'Report_Audit') {

                enableParam('company');
                enableParam('division');
                enableParam('name');
                enableParam('badge');
                enableParam('wildCardId');
                disableParam('wildCardText');
                disableParam('facility');
                enableParam('category');
                disableParam('area');
                disableParam('reader');

                misc1_ = bloodhoundInit('Status/2', 'Status/2');
                typeaheadInit('misc1', misc1_);

                enableParam('misc1');
                disableParam('misc2');

                $('#addClone').removeClass('addCb').addClass('addCb-disabled');
                $('#addClone').prop('src', 'resources/images/button_plus_blank.png');

                $('#misc1txt').text('Badge Status');
                $('#misc2txt').text('Disabled');

                $('#misc1txt').css('color', 'rgb(0,0,0)');
                $('#misc2txt').css('color', 'rgb(255,255,255)');

                $('#fromdate').prop('disabled', true);
                $('#fromtime').prop('disabled', true);
                $('#todate').prop('disabled', true);
                $('#totime').prop('disabled', true);
                $('.day').prop('disabled', true);
                $('.month').prop('disabled', true);

            } else if (_report == 'Report_AlarmStatus') {

                disableParam('company');
                disableParam('division');
                disableParam('name');
                disableParam('badge');
                disableParam('wildCardId');
                disableParam('wildCardText');
                enableParam('facility');
                disableParam('category');
                disableParam('area');
                disableParam('reader');

                misc1_ = bloodhoundInit('Alarm', 'Alarm');
                typeaheadInit('misc1', misc1_);

                misc2_ = bloodhoundInit('Inputs', 'Inputs');
                typeaheadInit('misc2', misc2_);

                enableParam('misc1');
                enableParam('misc2');

                $('#addClone').removeClass('addCb').addClass('addCb-disabled');
                $('#addClone').prop('src', 'resources/images/button_plus_blank.png');

                $('#misc1txt').text('Alarm Description');
                $('#misc2txt').text('Input Description');

                $('#misc1txt').css('color', 'rgb(0,0,0)');
                $('#misc2txt').css('color', 'rgb(0,0,0)');

                $('#fromdate').prop('disabled', false);
                $('#fromtime').prop('disabled', false);
                $('#todate').prop('disabled', false);
                $('#totime').prop('disabled', false);
                $('.day').prop('disabled', false);
                $('.month').prop('disabled', false);
            } else if (_report == 'Report_BadgeStatus') {

                enableParam('company');
                enableParam('division');
                enableParam('name');
                enableParam('badge');
                enableParam('wildCardId');
                disableParam('wildCardText');
                disableParam('facility');
                disableParam('category');
                disableParam('area');
                disableParam('reader');

                misc1_ = bloodhoundInit('Status/2', 'Status/2');
                typeaheadInit('misc1', misc1_);

                enableParam('misc1');
                disableParam('misc2');

                $('#addClone').removeClass('addCb').addClass('addCb-disabled');
                $('#addClone').prop('src', 'resources/images/button_plus_blank.png');

                $('#misc1txt').text('Badge Status');
                $('#misc2txt').text('Disabled');

                $('#misc1txt').css('color', 'rgb(0,0,0)');
                $('#misc2txt').css('color', 'rgb(255,255,255)');

                $('#fromdate').prop('disabled', false);
                $('#fromtime').prop('disabled', false);
                $('#todate').prop('disabled', false);
                $('#totime').prop('disabled', false);
                $('.day').prop('disabled', false);
                $('.month').prop('disabled', false);
            } else if (_report == 'Report_DoorCategory') {

                disableParam('company');
                disableParam('division');
                disableParam('name');
                disableParam('badge');
                disableParam('wildCardId');
                disableParam('wildCardText');
                disableParam('facility');
                enableParam('category');
                enableParam('area');
                disableParam('reader');

                misc1_ = bloodhoundInit('Door', 'Door');
                typeaheadInit('misc1', misc1_);

                enableParam('misc1');
                disableParam('misc2');

                $('#addClone').removeClass('addCb').addClass('addCb-disabled');
                $('#addClone').prop('src', 'resources/images/button_plus_blank.png');

                $('#misc1txt').text('Door');
                $('#misc2txt').text('Disabled');

                $('#misc1txt').css('color', 'rgb(0,0,0)');
                $('#misc2txt').css('color', 'rgb(255,255,255)');

                $('#fromdate').prop('disabled', true);
                $('#fromtime').prop('disabled', true);
                $('#todate').prop('disabled', true);
                $('#totime').prop('disabled', true);
                $('.day').prop('disabled', true);
                $('.month').prop('disabled', true);
            } else if (_report == 'Report_TopSoundingAlarm') {

                disableParam('company');
                disableParam('division');
                disableParam('name');
                disableParam('badge');
                disableParam('wildCardId');
                disableParam('wildCardText');
                disableParam('facility');
                disableParam('category');
                disableParam('area');
                disableParam('reader');

                misc1_ = bloodhoundInit('Misc', 'Misc');
                typeaheadInit('misc1', misc1_);

                enableParam('misc1');
                enableParam('misc2');

                $('#addClone').removeClass('addCb').addClass('addCb-disabled');
                $('#addClone').prop('src', 'resources/images/button_plus_blank.png');

                $('#misc1txt').text('Criteria');
                $('#misc2txt').text('Top n');

                $('#misc1txt').css('color', 'rgb(0,0,0)');
                $('#misc2txt').css('color', 'rgb(0,0,0)');

                $('#fromdate').prop('disabled', false);
                $('#fromtime').prop('disabled', false);
                $('#todate').prop('disabled', false);
                $('#totime').prop('disabled', false);
                $('.day').prop('disabled', false);
                $('.month').prop('disabled', false);
            }

            break;

        default:
    }
}

// Misc Globals

var criteriaArray = '';
var itemCount = 0;

/*
 * Helper Functions
 */

function getFromClones() {
    var cloneString = '';
    $('#cloneTable').find('span').each(function() {
        itemCount = itemCount + 1;
        var cloneStringNext = $(this).attr('id') + ';';
        cloneString += cloneStringNext;
    });
    return cloneString;
}

function mapDate(dateStr) {
    if (dateStr == '') return 'null';
    var monthStr = dateStr[0] + dateStr[1] + dateStr[2];
    var dayStr = dateStr[4] + dateStr[5];
    var yearStr = dateStr[7] + dateStr[8] + dateStr[9] + dateStr[10];

    var mmStr = '';
    switch (monthStr) {
        case 'Jan':
            mmStr = '01';
            break;
        case 'Feb':
            mmStr = '02';
            break;
        case 'Mar':
            mmStr = '03';
            break;
        case 'Apr':
            mmStr = '04';
            break;
        case 'May':
            mmStr = '05';
            break;
        case 'Jun':
            mmStr = '06';
            break;
        case 'Jul':
            mmStr = '07';
            break;
        case 'Aug':
            mmStr = '08';
            break;
        case 'Sep':
            mmStr = '09';
            break;
        case 'Oct':
            mmStr = '10';
            break;
        case 'Nov':
            mmStr = '11';
            break;
        case 'Dec':
            mmStr = '12';
            break;

    }

    return yearStr + mmStr + dayStr;
}

function mapTime(timeStr) {
    if (timeStr == '') return 'null';

    if (timeStr.length == 7) {
        timeStr = '0' + timeStr;
    }

    var hhStr = timeStr[0] + timeStr[1];
    var mmStr = timeStr[3] + timeStr[4];
    var ampmStr = timeStr[6] + timeStr[7];

    if (ampmStr == 'AM') {
        return hhStr + mmStr + '00';
    } else {
        var result = parseInt(hhStr, 10);
        var hh = 0
        if (result != 12)
            hh = result + 12;
        else
            hh = 12
        return hh.toString() + mmStr + '00';
    }
}

function mapCheckboxDays() {
    var days = '';
    var isDaySelected = 0;
    var allCheckBoxes = $('[name="allDays"]');

    if (allCheckBoxes[0].checked) {
        return 'null';
    }
    var checkboxes = $('[name="day"]');
    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            isDaySelected = 1;
            var intDay = i;
            days = days + intDay.toString() + ',';
        }
    }
    if (isDaySelected == 0)
        days = 'null';
    else
        days = days.substring(0, days.length - 1);

    return days;
}

function mapCheckBoxMonths() {
    var days = '';
    var checkboxes = $('[name="monthCheckBox"]');
    var isAll = 1;
    for (var i = 0; i < checkboxes.length; i++) {
        if (!checkboxes[i].checked) {
            isAll = 0;
        } else {
            days = days + checkboxes[i].value + ',';
        }
    }

    if (isAll == 1)
        return 'null';
    else {
        days = days.substring(0, days.length - 1);
        return days;
    }
}

function reloadGridDivision() {
    var grid = document.getElementById('gridContainer');
    while (grid.firstChild) {
        grid.removeChild(grid.firstChild);
    }

    var tableList = document.createElement('table');
    tableList.setAttribute('id', 'list');
    grid.appendChild(tableList);

    var divPager = document.createElement('div');
    divPager.setAttribute('id', 'pager');
    grid.appendChild(divPager);
}

function ExportData(tableCtrl, title, criteriaArray, type) {

    ExportJQGridDataToExcel(tableCtrl, title, type, criteriaArray, 'Trial.xlsx');
}


/*
 * View Report OnClick Handler
 */

$(document).on('click', '.viewRep', function() {

    criteriaArray = '';

    /*  
     * Parameter Collection
     */

    var areaString = getFromClones();

    var stDate = $('#fromdate').val();
    var endDate = $('#todate').val();
    var stTime = $('#fromtime').val();
    var endTime = $('#totime').val();

    /*  
     * Null Injection
     */

    areaString = (areaString == '') ? 'null,null,null,null' : areaString;

    /*  
     * Note: No null injection needed for DateTime
     */

    /*  
     * Criteria Collection
     */

    var employeeLbl = '';
    var emptype = '';

    if (company != 'null') {
        employeeLbl = $('#company').val();
        emptype = 'Company';
    }
    if (division != 'null') {
        employeeLbl = $('#division').val();
        emptype = 'Division';
    }
    if (name != 'null') {
        employeeLbl = $('#name').val();
        emptype = 'Name';
    }
    if (badge != 'null') {
        employeeLbl = $('#badge').val();
        emptype = 'Badge';
    }
    if (wildCardText != 'null') {
        employeeLbl = $('#wildCardText').val();
        emptype = $('#wildCardId').val();
    }
    if (emptype != '') {
        criteriaArray += emptype + ' : ';
        criteriaArray += employeeLbl + '       ';
    }

    var portalLbl = '';
    var portaltype = '';

    if (facility != 'null') {
        portalLbl = $('#facility').val();
        portaltype = 'Facility';
    }
    if (area != 'null') {
        portalLbl = $('#area').val();
        portaltype = 'Area';
    }
    if (category != 'null') {
        portalLbl = $('#category').val();
        portaltype = 'Category';
    }
    if (reader != 'null') {
        portalLbl = $('#reader').val();
        portaltype = 'Reader';
    }
    if (portaltype != '') {
        criteriaArray += portaltype + ' : ';
        criteriaArray += portalLbl + '       ';
    }


    if (stDate != '') {
        criteriaArray += 'Start Date' + ' : ';
        criteriaArray += stDate + '       ';
    }
    if (endDate != '') {
        criteriaArray += 'End Date' + ' : ';
        criteriaArray += endDate + '       ';
    }
    if (stTime != '') {
        criteriaArray += 'Start Time' + ' : ';
        criteriaArray += stTime + '       ';
    }
    if (endTime != '') {
        criteriaArray += 'End Time' + ' : ';
        criteriaArray += endTime + '       ';
    }

    var misc1Lbl = '';
    var misc1type = '';

    var misc2Lbl = '';
    var misc2type = '';

    var topntype = ($('#misc2').val()) ? $('#misc2').val() : 'null';

    if (misc1 != 'null') {
        misc1Lbl = $('#misc1').val();
        misc1type = $('#misc1txt').text();

        criteriaArray += misc1type + ' : ';
        criteriaArray += misc1Lbl + '       ';
    }

    if (misc2 != 'null') {
        misc2Lbl = $('#misc2').val();
        misc2type = $('#misc2txt').text();

        criteriaArray += misc2type + ' : ';
        criteriaArray += misc2Lbl + '       ';
    }

    else if (topntype != 'null') {
        misc2Lbl = topntype;
        topntype = 'null';
        misc2type = $('#misc2txt').text();

        criteriaArray += misc2type + ' : ';
        criteriaArray += misc2Lbl + '       ';
    }

    /*  
     * Map datetime parameteres to correct format
     */

    var days = mapCheckboxDays();
    var months = mapCheckBoxMonths();
    stDate = mapDate(stDate);
    endDate = mapDate(endDate);
    stTime = mapTime(stTime);
    endTime = mapTime(endTime);

    /*  
     * Start Report Generation
     */

    if (_report == 'Report_Activity') {
        if (itemCount <= 1) {
            var areaStringTxt = _facility + ',' + _area + ',' + _category + ',' + reader;
            var url = ROOTURL + _report + '/' + badge + '/' + _name + '/' + _company + '/' + _division + '/' + 'null' + '/' + _misc1 + '/' + areaStringTxt + '/' + stDate + '/' + stTime + '/' + endDate + '/' + endTime + '/' + days + '/' + months + '/' + _wildCardId + '/' + _wildCardText;
        }
            // Check this ----
        else {
            var url = ROOTURL + _report + '/' + badge + '/' + _name + '/' + _company + '/' + _division + '/' + 'null' + '/' + _misc1 + '/' + areaString + '/' + stDate + '/' + stTime + '/' + endDate + '/' + endTime + '/' + days + '/' + months + '/' + _wildCardId + '/' + _wildCardText;
        }
        // --------------
        reloadGridDivision();
        createGrid_Activity(url);
        jQuery('#list').jqGrid().trigger('reloadGrid');
    } else if (_report == 'Report_Audit') {
        var url = ROOTURL + _report + '/' + _badge + '/' + _name + '/' + _company + '/' + _division + '/' + 'null' + '/' + _category + '/' + _misc1 + '/' + _wildCardId + '/' + _wildCardText;
        reloadGridDivision();
        createGrid_Audit(url);
        jQuery('#list').jqGrid().trigger('reloadGrid');
    } else if (_report == 'Report_AlarmStatus') {
        var url = ROOTURL + 'Report_AlarmingDoor' + '/' + _facility + '/' + _misc1 + '/' + _misc2 + '/' + stDate + '/' + endDate + '/' + stTime + '/' + endTime + '/' + days + '/' + months;
        reloadGridDivision();
        createGrid_AlarmStatus(url);
        jQuery('#list').jqGrid().trigger('reloadGrid');
    } else if (_report == 'Report_BadgeStatus') {
        var url = ROOTURL + _report + '/' + _badge + '/' + _name + '/' + _company + '/' + _division + '/' + 'null' + '/' + _misc1 + '/' + stDate + '/' + endDate + '/' + days + '/' + months + '/' + _wildCardId + '/' + _wildCardText;
        reloadGridDivision();
        createGrid_BadgeStatus(url);
        jQuery('#list').jqGrid().trigger('reloadGrid');
    } else if (_report == 'Report_DoorCategory') {
        var url = ROOTURL + _report + '/' + _area + '/' + _category + '/' + _misc1;
        reloadGridDivision();
        createGrid_DoorCategory(url);
        jQuery('#list').jqGrid().trigger('reloadGrid');
    } else if (_report == 'Report_TopSoundingAlarm') {
        var topn = $('#misc2').val();
        var url = ROOTURL + _report + '/' + _misc1 + '/' + topn + '/' + stDate + '/' + stTime + '/' + endDate + '/' + endTime;
        reloadGridDivision();
        createGrid_TopSoundingAlarms(url);
        jQuery('#list').trigger('reloadGrid');
    }
});

function createGrid_DoorCategory(url) {
    $('#list').jqGrid({
        datatype: 'json',
        url: url,
        loadtext: 'Generating Report. Your patience is appreciated.',
        ignoreCase: true,
        colNames: ['Area', 'Door', 'Category'],
        colModel: [{
            name: 'area',
            index: 'area',
            width: 150,
            align: 'left'
        }, {
            name: 'door',
            index: 'door',
            width: 150,
            align: 'left'
        }, {
            name: 'category',
            index: 'category',
            width: 150,
            align: 'left'
        }, ],
        jsonReader: {
            root: 'report.repdata',
            page: 'page',
            total: 'total',
            records: 'records',
            repeatitems: true,
            id: 'id',
            cell: 'datarow'
        },
        loadonce: true,
        pager: '#pager',
        rowNum: 50,
        rowList: [50, 100],
        sortname: 'Area',
        sortorder: 'desc',
        viewrecords: true,
        autowidth: true,
        height: 525,
        autoencode: true,
        gridview: true
    });

    $('#list').jqGrid('filterToolbar', { searchOnEnter: false, defaultSearch: 'cn'});

    $('#list').jqGrid('navGrid', '#pager', {
        search: true,
        searchtext: 'Search', //  Make the Search icon have a 'Search' label next to it
        edit: false,
        add: false,
        del: false,
        refresh: false
    }, {}, // default settings for edit
        {}, // default settings for add
        {}, // delete
        {
            closeOnEscape: true,
            closeAfterSearch: true,
            ignoreCase: true,
            multipleSearch: false,
            multipleGroup: false,
            showQuery: false,
            sopt: ['cn', 'eq', 'ne'],
            defaultSearch: 'cn'
        }).navButtonAdd('#pager', {
            caption: 'Export to PDF',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Door Category Report', criteriaArray, 'pdf');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Word',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Door Category Report', criteriaArray, 'word');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Excel',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Door Category Report', criteriaArray, 'excel');
            },
            position: 'last'
        });


    function ExportPDF() {
        ExportData('#list');
    }
}

function createGrid_AlarmStatus(url) {
    $('#list').jqGrid({
        datatype: 'json',
        url: url,
        loadtext: 'Generating Report. Your patience is appreciated.',
        ignoreCase: true,
        colNames: ['Input Group', 'Door', 'Alarm', 'Facility', 'Occurrence Datetime', 'Day'],
        colModel: [{
            name: 'inputGroup',
            index: 'inputGroup',
            width: 50,
            align: 'left',
            sorttype: 'int'
        }, {
            name: 'inputDesc',
            index: 'inputDesc',
            width: 200,
            align: 'left'
        }, {
            name: 'alarm',
            index: 'alarm',
            width: 200,
            align: 'left'
        }, {
            name: 'facility',
            index: 'facility',
            width: 200,
            align: 'left'
        }, {
            name: 'happenedDateTime',
            index: 'happenedDateTime',
            width: 200,
            align: 'center',
            sorttype: 'date'
        }, {
            name: 'day',
            index: 'day',
            width: 100,
            align: 'center'
        }],
        jsonReader: {
            root: 'report.repdata',
            page: 'page',
            total: 'total',
            records: 'records',
            repeatitems: true,
            id: 'id',
            cell: 'datarow'
        },
        loadonce: true,
        pager: '#pager',
        rowNum: 50,
        rowList: [50, 100],
        sortname: 'Happened Datetime',
        sortorder: 'desc',
        viewrecords: true,
        autowidth: true,
        height: 525,
        autoencode: true,
        gridview: true
    });
    $('#list').jqGrid('filterToolbar', { searchOnEnter: false, defaultSearch: 'cn' });
    $('#list').jqGrid('navGrid', '#pager', {
        search: true,
        searchtext: 'Search', //  Make the Search icon have a 'Search' label next to it
        edit: false,
        add: false,
        del: false,
        refresh: false
    }, {}, // default settings for edit
        {}, // default settings for add
        {}, // delete
        {
            closeOnEscape: true,
            closeAfterSearch: true,
            ignoreCase: true,
            multipleSearch: false,
            multipleGroup: false,
            showQuery: false,
            sopt: ['cn', 'eq', 'ne'],
            defaultSearch: 'cn'
        }).navButtonAdd('#pager', {
            caption: 'Export to PDF',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Alarming Door Report', criteriaArray, 'pdf');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Word',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Alarming Door Report', criteriaArray, 'word');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Excel',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Alarming Door Report', criteriaArray, 'excel');
            },
            position: 'last'
        });


    function ExportPDF() {
        ExportData('#list');
    }
}

function createGrid_BadgeStatus(url) {
    $('#list').jqGrid({
        datatype: 'json',
        url: url,
        loadtext: 'Generating Report. Your patience is appreciated.',
        ignoreCase: true,
        colNames: ['Employee ID', 'First Name', 'Last Name', 'Company', 'Division', 'Badge', 'Badge Status', 'Expired Datetime', 'Return Datetime'],
        colModel: [{
            name: 'employeeID',
            index: 'employeeID',
            width: 100,
            align: 'center',
            sorttype: 'int'
        }, {
            name: 'firstName',
            index: 'firstName',
            width: 100,
            align: 'left'
        }, {
            name: 'lastName',
            index: 'lastName',
            width: 100,
            align: 'left'
        }, {
            name: 'company',
            index: 'company',
            width: 200,
            align: 'left'
        }, {
            name: 'division',
            index: 'division',
            width: 200,
            align: 'left'
        }, {
            name: 'badge',
            index: 'badge',
            width: 100,
            align: 'center',
            sorttype: 'int'
        }, {
            name: 'badgeStatus',
            index: 'badgeStatus',
            width: 50,
            align: 'center'
        }, {
            name: 'expiredDateTime',
            index: 'expiredDateTime',
            width: 100,
            align: 'center',
            sorttype: 'date'
        }, {
            name: 'returnDateTime',
            index: 'returnDateTime',
            width: 100,
            align: 'center',
            sorttype: 'date'
        }],
        jsonReader: {
            root: 'report.repdata',
            page: 'page',
            total: 'total',
            records: 'records',
            repeatitems: true,
            id: 'id',
            cell: 'datarow'
        },
        loadonce: true,
        pager: '#pager',
        rowNum: 50,
        rowList: [50, 100],
        sortname: 'Badge',
        sortorder: 'desc',
        viewrecords: true,
        autowidth: true,
        height: 525,
        autoencode: true,
        gridview: true
    });
    $('#list').jqGrid('filterToolbar', { searchOnEnter: false, defaultSearch: 'cn' });
    $('#list').jqGrid('navGrid', '#pager', {
        search: true,
        searchtext: 'Search', //  Make the Search icon have a 'Search' label next to it
        edit: false,
        add: false,
        del: false,
        refresh: false
    }, {}, // default settings for edit
        {}, // default settings for add
        {}, // delete
        {
            closeOnEscape: true,
            closeAfterSearch: true,
            ignoreCase: true,
            multipleSearch: false,
            multipleGroup: false,
            showQuery: false,
            sopt: ['cn', 'eq', 'ne'],
            defaultSearch: 'cn'
        }).navButtonAdd('#pager', {
            caption: 'Export to PDF',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Badge Status Report', criteriaArray, 'pdf');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Word',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Badge Status Report', criteriaArray, 'word');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Excel',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Badge Status Report', criteriaArray, 'excel');
            },
            position: 'last'
        });


    function ExportPDF() {
        ExportData('#list');
    }
}

function createGrid_TopSoundingAlarms(url) {

    var columnName;
    if(_misc1 == 'alarm_desc') {
        columnName = 'Alarm Description';
    }
    else if (_misc1 == 'input_desc') {
        columnName = 'Input Description';
    }
    else {
        columnName = 'Facility';
    }
    $('#list').jqGrid({
        datatype: 'json',
        url: url,
        loadtext: 'Generating Report. Your patience is appreciated.',
        ignoreCase: true,
        colNames: [columnName, 'Count'],
        colModel: [{
            name: 'criteria',
            index: 'criteria',
            width: 500,
            align: 'left'
        }, {
            name: 'count',
            index: 'count',
            width: 100,
            align: 'center',
            sorttype: 'int'
        }],
        jsonReader: {
            root: 'report.repdata',
            page: 'page',
            total: 'total',
            records: 'records',
            repeatitems: true,
            id: 'id',
            cell: 'datarow'
        },
        loadonce: true,
        pager: '#pager',
        rowNum: 50,
        rowList: [50, 100],
        sortname: 'Count',
        sortorder: 'desc',
        viewrecords: true,
        autowidth: true,
        height: 525,
        autoencode: true,
        gridview: true
    });
    $('#list').jqGrid('filterToolbar', { searchOnEnter: false, defaultSearch: 'cn' });
    $('#list').jqGrid('navGrid', '#pager', {
        search: true,
        searchtext: 'Search', //  Make the Search icon have a 'Search' label next to it
        edit: false,
        add: false,
        del: false,
        refresh: false
    }, {}, // default settings for edit
        {}, // default settings for add
        {}, // delete
        {
            closeOnEscape: true,
            closeAfterSearch: true,
            ignoreCase: true,
            multipleSearch: false,
            multipleGroup: false,
            showQuery: false,
            sopt: ['cn', 'eq', 'ne'],
            defaultSearch: 'cn'
        }).navButtonAdd('#pager', {
            caption: 'Export to PDF',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Top Sounding Alarms', criteriaArray, 'pdf');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Word',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Top Sounding Alarms', criteriaArray, 'word');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Excel',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Top Sounding Alarms', criteriaArray, 'excel');
            },
            position: 'last'
        });


    function ExportPDF() {
        ExportData('#list');
    }
}

function createGrid_Audit(url) {
    $('#list').jqGrid({
        datatype: 'json',
        url: url,
        loadtext: 'Generating Report. Your patience is appreciated.',
        ignoreCase: true,
        colNames: ['Company Name', 'Division', 'Category', 'First Name', 'Last Name', 'Employee ID', 'Badge', 'Badge Status' ],
        colModel: [{
            name: 'companyName',
            index: 'companyName',
            width: 200,
            align: 'left'
        }, {
            name: 'division',
            index: 'division',
            width: 200,
            align: 'left'
        },
            {
                name: 'category',
                index: 'category',
                width: 200,
                align: 'left'
            },
            {
                name: 'firstName',
                index: 'firstName',
                width: 120,
                align: 'left'
            },

            {
                name: 'lastName',
                index: 'lastName',
                width: 120,
                align: 'left'
            }, {
                name: 'empId',
                index: 'empId',
                width: 100,
                align: 'center',
                sorttype: 'int'
            },

            {
                name: 'badge',
                index: 'badge',
                width: 80,
                align: 'center',
                sorttype: 'int'
            },
             {
                 name: 'status',
                 index: 'status',
                 width: 80,
                 align: 'left'
             }
        ],
        jsonReader: {
            root: 'report.repdata',
            page: 'page',
            total: 'total',
            records: 'records',
            repeatitems: true,
            id: 'id',
            cell: 'datarow'
        },
        loadonce: true,
        pager: '#pager',
        rowNum: 50,
        rowList: [50, 100],
        sortname: 'Badge',
        sortorder: 'desc',
        viewrecords: true,
        autowidth: true,
        height: 525,
        autoencode: true,
        gridview: true,
    });
    $('#list').jqGrid('filterToolbar', { searchOnEnter: false, defaultSearch: 'cn' });

    $('#list').jqGrid('navGrid', '#pager', {
        search: true,
        searchtext: 'Search', //  Make the Search icon have a 'Search' label next to it
        edit: false,
        add: false,
        del: false,
        refresh: false
    }, {}, // default settings for edit
        {}, // default settings for add
        {}, // delete
        {
            closeOnEscape: true,
            closeAfterSearch: true,
            ignoreCase: true,
            multipleSearch: false,
            multipleGroup: false,
            showQuery: false,
            sopt: ['cn', 'eq', 'ne'],
            defaultSearch: 'cn'
        }).navButtonAdd('#pager', {
            caption: 'Export to PDF',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Audit Report', criteriaArray, 'pdf');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Word',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Audit Report', criteriaArray, 'word');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Excel',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Audit Report', criteriaArray, 'excel');
            },
            position: 'last'
        });


    function ExportPDF() {
        ExportData('#list');
    }
}

function createGrid_Activity(url) {
    $('#list').jqGrid({
        datatype: 'json',
        url: url,
        loadtext: 'Generating Report. Your patience is appreciated.',
        ignoreCase: true,
        colNames: ['Badge', 'Company', 'First Name', 'Last Name', 'Reader', 'Status', 'Access Time'],
        colModel: [{
            name: 'badge',
            index: 'badge',
            width: 75,
            align: 'center',
            sorttype: 'int'
        }, {
            name: 'company',
            index: 'company',
            width: 150,
            align: 'left'
        }, {
            name: 'firstName',
            index: 'firstName',
            width: 75,
            align: 'left'
        }, {
            name: 'lastName',
            index: 'lastName',
            width: 75,
            align: 'left'
        },{
            name: 'reader',
            index: 'reader',
            width: 150,
            align: 'left'
        },  {
            name: 'status',
            index: 'status',
            width: 50,
            align: 'left'
        }, {
            name: 'dateHistory',
            index: 'dateHistory',
            width: 150,
            align: 'center',
            sorttype: 'date'
        }],

        jsonReader: {
            root: 'report.repdata',
            page: 'page',
            total: 'total',
            records: 'records',
            repeatitems: true,
            id: 'id',
            cell: 'datarow'
        },
        loadonce: true,
        pager: '#pager',
        rowNum: 50,
        rowList: [50, 100],
        sortname: 'Badge',
        sortorder: 'desc',
        viewrecords: true,
        autowidth: true,
        height: 525,
        autoencode: true,
        gridview: true
    });
    $('#list').jqGrid('filterToolbar', { searchOnEnter: false, defaultSearch: 'cn' });


    $('#list').jqGrid('navGrid', '#pager', {
        search: true,
        searchtext: 'Search', //  Make the Search icon have a 'Search' label next to it
        edit: false,
        add: false,
        del: false,
        refresh: false
    }, {}, // default settings for edit
        {}, // default settings for add
        {}, // delete
        {
            closeOnEscape: true,
            closeAfterSearch: true,
            ignoreCase: true,
            multipleSearch: false,
            multipleGroup: false,
            showQuery: false,
            sopt: ['cn', 'eq', 'ne'],
            defaultSearch: 'cn'
        }).navButtonAdd('#pager', {
            caption: 'Export to PDF',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Activity Report', criteriaArray, 'pdf');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Word',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Activity Report', criteriaArray, 'word');
            },
            position: 'last'
        }).navButtonAdd('#pager', {
            caption: 'Export to Excel',
            buttonicon: 'ui-icon-disk',
            onClickButton: function() {
                ExportData('#list', 'Activity Report', criteriaArray, 'excel');
            },
            position: 'last'
        });


    function ExportPDF() {
        ExportData('#list');
    }
}

/*
$(document).ready(function () {
    disableParam('company');
    disableParam('division');
    disableParam('name');
    disableParam('badge');
    disableParam('wildCardId');
    disableParam('wildCardText');
    disableParam('facility');
    disableParam('category');
    disableParam('area');
    disableParam('reader');
    disableParam('misc1');
    disableParam('misc2');

    $('#misc1txt').text('');
    $('#misc2txt').text('');

    $('#fromdate').prop('disabled', true);
    $('#fromtime').prop('disabled', true);
    $('#todate').prop('disabled', true);
    $('#totime').prop('disabled', true);
    $('.day').prop('disabled', true);
    $('.month').prop('disabled', true);
});
*/