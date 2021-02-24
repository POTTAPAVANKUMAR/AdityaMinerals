/**
 * retain the current pagination settings depanding upon the row number.
 *
 *
 *  @example
 *    $(document).ready(function() {
 *        var table = $('.dataTable').dataTable()
 *        table.fnGetPageOfRow();
 *    } );
 */


// get the page of a given item in order to paginate to it's page on load
jQuery.fn.dataTableExt.oApi.fnGetPageOfRow = function (oSettings, iRow) {
    // get the displayLength being used
    var displayLength = oSettings._iDisplayLength;

    // get the array of nodes, sorted (default) and using current filters in place for all pages (default)
    // see http://datatables.net/docs/DataTables/1.9.beta.1/DataTable.html#%24_details for more detals
    var taskListItems = this.$("tr", { "filter": "applied" });

    // if there's more than one page continue, else do nothing
    if (taskListItems.length <= displayLength) return;

    // get the index of the row inside that sorted/filtered array
    var index = taskListItems.index(iRow);

    // get the page by removing the decimals
    var page = Math.floor(index / displayLength);

    // paginate to that page 
    this.fnPageChange(page);
};
