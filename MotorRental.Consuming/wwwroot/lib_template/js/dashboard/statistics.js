(function($) {
    /* "use strict" */


 var dzChartlist = function(){
	//let draw = Chart.controllers.line.__super__.draw; //draw shadow
	var screenWidth = $(window).width();
	var donutChart2 = function(){
		$("span.donut2").peity("donut", {
			width: "110",
			height: "110"
		});
	}
	var radialBar = function(){
		var optionsCircle = {
		  chart: {
			type: 'radialBar',
			//width:320,
			height: 350,
			offsetY: 0,
			offsetX: 0,
			
		  },
		  plotOptions: {
			radialBar: {
				startAngle: -150,
				endAngle: 200,
			  size: undefined,
			  inverseOrder: false,
			  hollow: {
				margin: 0,
				size: '20%',
				background: 'transparent',
			  },
			  
			  
			  
			  track: {
				show: true,
				background: '#e1e5ff',
				strokeWidth: '10%',
				opacity: 1,
				margin: 18, // margin is in pixels
			  },


			},
		  },
		  responsive: [{
          breakpoint: 480,
          options: {
			chart: {
			offsetY: 0,
			offsetX: 0
		  },	
            legend: {
              position: 'bottom',
              offsetX:0,
              offsetY: 0
            }
          }
        }],
		
		fill: {
          opacity: 1
        },
		stroke: {
          lineCap: 'round'
        },
		colors:['#2BC155','#FF9B52' ],
		series: [73, 64],
		labels: ['Following', 'Followers'], 
		legend: {
			fontSize: '16px',  
			show: false,
		  },		 
		}

		var chartCircle1 = new ApexCharts(document.querySelector('#radialBar'), optionsCircle);
		chartCircle1.render();
	}
	
	var chartBar = function(){
		var options = {
          series: [{
          name: 'Income',
          data: [420, 550, 650, 220, 650, 470, 310, 700, 290, 470, 370, 300, 280, 500, 580, 270, 470, 370]
        }, {
          name: 'Expenses',
          data: [270, 650, 201, 90, 250, 750, 470, 550, 650, 270, 570, 450, 350, 270, 290, 470, 270, 600]
        }],
          chart: {
          type: 'bar',
          height: 250,
		  toolbar: {
            show: false
          },
			
        },
        plotOptions: {
          bar: {
            horizontal: false,
            columnWidth: '55%',
            endingShape: 'rounded',
			borderRadius:5,
          },
        },
        dataLabels: {
          enabled: false
        },
		grid:{
			borderColor:'transparent'
		},
		legend: {
			show: false,
			fontSize: '12px',
			fontWeight: 300,
			
			labels: {
				colors: 'black',
			},
			position: 'bottom',
			horizontalAlign: 'center', 	
			markers: {
				width: 19,
				height: 19,
				strokeWidth: 0,
				radius: 19,
				strokeColor: '#fff',
				fillColors:['#8743DF','#2BC155'],
				offsetX: 0,
				offsetY: 0
			}
		},
		yaxis: {
			show: false
		},
        stroke: {
          show: true,
          width: 5,
          colors: ['transparent']
        },
        xaxis: {
          categories: ['01','02','03','04','05','06', '07', '08', '09', '10','11','12','13','14','15','16','17','18'],
		  labels: {
		   style: {
			  colors: '#808080',
			  fontSize: '14px',
			  fontFamily: 'Poppins',
			  fontWeight: 100,
			  
			},
		  },
        },
		colors:['#8743DF','#2BC155'],
        fill: {
		  colors:['#8743DF','#2BC155'],
          opacity: 1
        },
        tooltip: {
          y: {
            formatter: function (val) {
              return "$ " + val + " thousands"
            }
          }
        },
		responsive: [{
			breakpoint: 575,
			options: {
				xaxis: {
				   labels: {
				   style: {
					  fontSize: '9px'
					  
					},
				  },
				},
			},
		}]

        };

        var chart = new ApexCharts(document.querySelector("#chartBar"), options);
        chart.render();	
	}
	var stackedChart = function(){
		var options = {
          series: [{
          name: 'Aplication Sent',
          data: [20]
        }, {
          name: 'Appllication Answered',
          data: [30]
        }, {
          name: 'Hired',
          data: [15]
        }, {
          name: 'Pending',
          data: [25]
        }],
          chart: {
          type: 'bar',
          height: 90,
          stacked: true,
		  toolbar:{
			show:false,
		  },
          stackType: '100%'
        },
        plotOptions: {
          bar: {
            horizontal: true,
            endingShape: 'rounded',
            startingShape: 'rounded'
          },
        },
		grid:{
			show:false,
		},
        stroke: {
          width: 0,
          colors: ['#fff']
        },
		colors:['#2BC155','#FF9B52','#3F9AE0','#C4C4C4'],
		dataLabels:{
			enabled:false,
		},
		xaxis: {
				show: false,
				lines: {
					show: false,
				},
				labels: {
					show: false,
				},
				axisBorder: {
				  show: false,
				},
				
			},
			yaxis: {
				show: false,
				
			},
        tooltip: {
          y: {
            formatter: function (val) {
              return val + "K"
            }
          }
        },
        fill: {
          opacity: 1
        
        },
        legend: {
			show:false,
         
        }
        };	

        var chart = new ApexCharts(document.querySelector("#stackedChart"), options);
        chart.render();
	}
	/* Function ============ */
		return {
			init:function(){
			},
			
			
			load:function(){
				donutChart2();
				radialBar();
				chartBar();
				stackedChart();
			},
			
			resize:function(){
				
			}
		}
	
	}();

	jQuery(document).ready(function(){
	});
		
	jQuery(window).on('load',function(){
		setTimeout(function(){
			dzChartlist.load();
		}, 1000); 
		
	});

	jQuery(window).on('resize',function(){
		
		
	});     

})(jQuery);