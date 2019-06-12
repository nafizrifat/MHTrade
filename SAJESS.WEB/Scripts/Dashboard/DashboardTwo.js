$(document).ready(function () {

    var data = [
        {
            "category": "category 1",
            "column-1": 8
        },
        {
            "category": "category 2",
            "column-1": 16
        },
        {
            "category": "category 3",
            "column-1": 2
        },
        {
            "category": "category 4",
            "column-1": 7
        },
        {
            "category": "category 5",
            "column-1": 5
        },
        {
            "category": "category 6",
            "column-1": 9
        },
        {
            "category": "category 7",
            "column-1": 4
        },
        {
            "category": "category 8",
            "column-1": 15
        },
        {
            "category": "category 9",
            "column-1": 12
        },
        {
            "category": "category 10",
            "column-1": 17
        },
        {
            "category": "category 11",
            "column-1": 18
        },
        {
            "category": "category 12",
            "column-1": 21
        },
        {
            "category": "category 13",
            "column-1": 24
        },
        {
            "category": "category 14",
            "column-1": 23
        },
        {
            "category": "category 15",
            "column-1": 24
        }
    ];

   // loadChart();
    var data1 = null;
    $.ajax({
        url: '/Dashboard/GetSupplierWiseTotalPaymentDashboard',
        type: 'GET',
        success: function (response) {
            debugger;
            data1 = response;
            loadChart();
        },
        error: function (error) {
            $(this).remove();
            DisplayError(error.statusText);
        }
    });

    function loadChart() {
        AmCharts.makeChart("chartdiv2",
                  {
                      "type": "serial",
                      "categoryField": "category",
                      "startDuration": 1,
                      "categoryAxis": {
                          "autoRotateAngle": 45,
                          "gridPosition": "start",
                          "autoGridCount": false,
                          "axisAlpha": 0.02,
                          "offset": -2
                      },
                      "chartCursor": {
                          "enabled": true
                      },
                      "chartScrollbar": {
                          "enabled": true
                      },
                      "trendLines": [],
                      "graphs": [
                          {
                              "fillAlphas": 1,
                              "id": "AmGraph-1",
                              "title": "graph 1",
                              "type": "column",
                              "valueField": "value"
                          }
                      ],
                      "guides": [],
                      "valueAxes": [
                          {
                              "id": "ValueAxis-1",
                              "title": "Axis title"
                          }
                      ],
                      "allLabels": [],
                      "balloon": {},
                      "titles": [
                          {
                              "id": "Title-1",
                              "size": 15,
                              "text": "Supplier Wise Total Payment"
                          }
                      ],
                      "dataProvider": data1
                  }
              );
  
    }

    
   

  

});