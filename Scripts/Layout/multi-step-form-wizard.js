"use strict";
(function( $ ) {
	
	function scroll_to_class(element_class, removed_height) {
		var scroll_to = $(element_class).offset().top - removed_height;
		if($(window).scrollTop() != scroll_to) {
			$('.form-wizard').stop().animate({scrollTop: scroll_to}, 0);
		}
	}

	function bar_progress(progress_line_object, direction) {
		//alert("ff")
		var number_of_steps = progress_line_object.data('number-of-steps');
		var now_value = progress_line_object.data('now-value');
		var new_value = 0;
		if(direction == 'right') {
			new_value = now_value + ( 100 / number_of_steps );
		}
		else if(direction == 'left') {
			new_value = now_value - ( 100 / number_of_steps );
		}
		progress_line_object.attr('style', 'width: ' + new_value + '%;').data('now-value', new_value);
	}

	jQuery(document).ready(function() {
		
		/*
			Form
		*/
		$('.form-wizard fieldset:first').fadeIn('slow');
		
		$('.form-wizard .required').on('focus', function() {
			$(this).removeClass('input-error');
		});
		
	    //Personal Details
        $('#Step1_Demographics').on('click', function () {          
		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            next_step = false;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });
		    // fields validation            
		    if (validateStep1() == false)
		        next_step = false;
		    //else if (SaveDemographics('Next') == false)
		    //    next_step = false;
		    else
		        next_step = true;

		    if (next_step) {
		        var EIPID = $("#EIP_ID").val();	       
              
                if (EIPID == null || EIPID == "0" || EIPID == "") {
                    
                    var WorkerName_VC = $('#WorkerName').val();
                    var WorkerId = $("#workerID").val();
              
                    var FatherorSpouseName = $('#FatherorSpouseName').val();
                    var dateofBirth = $('#dateofBirth').val();
                    var IDProofType = $("#drpIDProofType option:selected").text();
                    IDProofType = (IDProofType != "- ID Proof Type -") ? $("#drpIDProofType option:selected").text() : null;
                    var IDProofNum = $("#txtIdProofNum").val();

                    var objReq = {
                        WorkerName: WorkerName_VC,
                        WorkerFatherName: FatherorSpouseName,
                        DateOfBirth: dateofBirth,
                        IDProofNum: IDProofNum,
                        IDProofType: IDProofType,
                        WorkerId: WorkerId,
                        ValidateType: "AD",
                    }
                    //$('#divLoading').show();
                    $.ajax({
                        type: "POST",
                        cache: false,
                        async: true,
                        //dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        url: window.CheckDuplicateWorkerURL,
                        data: JSON.stringify({ objReq: objReq }),
                        success: function (data) {
                            
                            if (data.status === "Duplicate") {
                                $("#lblSuccessmsgWisaId").text(data.message);
                                if (data.wisaId !== 0)
                                {
                                    if (data.isEIPData == true) {

                                        window.redirectUrlEIPWorker += "?Worker_ID=" + data.wisaId + "&isFromAPI=True";

                                        $("#wisaIdsuccessbold,#wisaIdsuccess").show();
                                        $("#wisaIdsuccess").removeAttr("href");
                                        $("#wisaIdsuccess").attr('href', window.redirectUrlEIPWorker);
                                        //$("#wisaIdsuccess").attr('href', "/InductionApproval/WorkerDetailsFromDashboard?Worker_ID=" + data.wisaId + "&isFromAPI=True");
                                        $("#wisaIdsuccess").text("EIP ID: " + data.wisaId);
                                    }
                                    else {
                                        $("#wisaIdsuccessbold,#wisaIdsuccess").show();
                                        $("#wisaIdsuccess").removeAttr("href");
                                        if (data.workerRecordStatus != "Draft") {

                                            window.redirectUrlNonDraftWorker += "?Worker_ID=" + data.wisaId;

                                            $("#wisaIdsuccess").attr('href', window.redirectUrlNonDraftWorker);
                                            //$("#wisaIdsuccess").attr('href', "/InductionApproval/WorkerDetails?Worker_ID=" + data.wisaId);
                                            $("#wisaIdsuccess").text("WISA ID: " + data.wisaId);
                                        }
                                        if (data.workerRecordStatus == "Draft") {

                                            window.redirectUrlDraftWorker += "?WorkerID=" + data.wisaId;

                                            $("#wisaIdsuccess").attr('href', window.redirectUrlDraftWorker);
                                            //$("#wisaIdsuccess").attr('href', "/WorkmenInduction/WorkmenInductionDetails?WorkerID=" + data.wisaId);
                                            $("#wisaIdsuccess").text("WISA ID: " + data.wisaId);
                                        }
                                    }
                                    
                                }
                                $("#btnSuccessAlertWisaId").click();
                                // isValid = false;
                                // $("#duplicateFormId").val(data.message);
                            }
                            else {
                                if (SaveDemographics('Next') == true) {
                                    parent_fieldset.fadeOut(400, function () {
                                        // change icons
                                        //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
                                        $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
                                        $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
                                        current_active_step.next().removeClass("activated");
                                        current_active_step.next().addClass("active");
                                        // progress bar
                                        //bar_progress(progress_line, 'right');
                                        var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
                                        var total_steps = $(".form-wizard-steps .form-wizard-step").length;
                                        $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
                                        $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

                                        // show next step
                                        $(this).next().fadeIn();
                                        // scroll window to beginning of the form
                                        scroll_to_class($('.form-wizard'), 20);
                                    });
                                }
                            }

                        }
                    });
                    $('#msgIdProofNum').html('').hide();
                }
                else {
                    if (SaveDemographics('Next') == true) {
                        parent_fieldset.fadeOut(400, function () {
                            // change icons
                            //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
                            $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
                            $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
                            current_active_step.next().removeClass("activated");
                            current_active_step.next().addClass("active");
                            // progress bar
                            //bar_progress(progress_line, 'right');
                            var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
                            var total_steps = $(".form-wizard-steps .form-wizard-step").length;
                            $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
                            $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

                            // show next step
                            $(this).next().fadeIn();
                            // scroll window to beginning of the form
                            scroll_to_class($('.form-wizard'), 20);
                        });
                    }
                }
		    }
		});

	    //KYC Details
       // $('#Step2_KYC').on('click', function () {  
        $('body').on('click', '#Step2_KYC', function () {
		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            next_step = false;
		            //next_step = true;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });
		    // fields validation       
		    if (validateStep2() == false)
                next_step = false;
            else if (!validateKYCDetail()) {
                //debugger
                next_step = false;
            }
		    else if (SaveKYC('Next') == false)
		        next_step = false;
		    else
		        next_step = true;

		    if (next_step) {
		        parent_fieldset.fadeOut(400, function () {
		            // change icons
		            //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
		            $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
		            $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
		            current_active_step.next().removeClass("activated");
		            current_active_step.next().addClass("active");
		            // progress bar
		            //bar_progress(progress_line, 'right');
		            var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
		            var total_steps = $(".form-wizard-steps .form-wizard-step").length;
		            $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
		            $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

		            // show next step
		            $(this).next().fadeIn();
		            // scroll window to beginning of the form
		            scroll_to_class($('.form-wizard'), 20);
		        });
		    }
		});

	    //Education Details	    
		$('#Step3_Education').on('click', function () {
		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            next_step = false;
		            //next_step = true;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });
		    // fields validation              
		    if (validateStep3() == false)
		        next_step = false;
		    else if (SaveEducationDetails('Next') == false)
		        next_step = false;
		    else
		        next_step = true;

		    if (next_step) {
		        parent_fieldset.fadeOut(400, function () {
		            // change icons
		            //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
		            $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
		            $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
		            current_active_step.next().removeClass("activated");
		            current_active_step.next().addClass("active");
		            // progress bar
		            //bar_progress(progress_line, 'right');
		            var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
		            var total_steps = $(".form-wizard-steps .form-wizard-step").length;
		            $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
		            $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

		            // show next step
		            $(this).next().fadeIn();
		            // scroll window to beginning of the form
		            scroll_to_class($('.form-wizard'), 20);
		        });
		    }
		});

	    //Certification Details
        $('body').on('click', '#Step4_Certification', function () {
        //$('#Step4_Certification').on('click', function () {
            debugger;
		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            //next_step = false;
		            next_step = true;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });
		    // fields validation      
		    if (validateStep4() == false)
		        next_step = false;
		    else if (SaveCertificationDetails() == false)
		        next_step = false;
		    else
		        next_step = true;

		    if (next_step) {
		        parent_fieldset.fadeOut(400, function () {
		            // change icons
		            //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
		            $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
		            $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
		            current_active_step.next().removeClass("activated");
		            current_active_step.next().addClass("active");
		            // progress bar
		            //bar_progress(progress_line, 'right');
		            var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
		            var total_steps = $(".form-wizard-steps .form-wizard-step").length;
		            $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
		            $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

		            // show next step
		            $(this).next().fadeIn();
		            // scroll window to beginning of the form
		            scroll_to_class($('.form-wizard'), 20);
		        });
		    }
		});
		
	    //Bank Details
		$('#Step5_Bank').on('click', function () {
		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            next_step = false;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });
		    // fields validation      
		    if (validateStep5() == false)
		        next_step = false;
		    else if (SaveBankDetails('Next') == false)
		        next_step = false;
		    else
		        next_step = true;

		    if (next_step) {
		        parent_fieldset.fadeOut(400, function () {
		            // change icons
		            //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
		            $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
		            $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
		            current_active_step.next().removeClass("activated");
		            current_active_step.next().addClass("active");
		            // progress bar
		            //bar_progress(progress_line, 'right');
		            var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
		            var total_steps = $(".form-wizard-steps .form-wizard-step").length;
		            $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
		            $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

		            // show next step
		            $(this).next().fadeIn();
		            // scroll window to beginning of the form
		            scroll_to_class($('.form-wizard'), 20);
		        });
		    }
		});

	    //Work Experience
		$('#Step6_Experience').on('click', function () {
		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            next_step = true;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });

		    //fields validation   
		    if (validateStep6() == false) {
		        next_step = false;
		    }
		    else if (SaveExperienceDetails("Next") == false)
		        next_step = false;
		    else
		        next_step = true;

		    if (next_step) {
		        parent_fieldset.fadeOut(400, function () {
		            // change icons
		            //current_active_step.removeClass('active').addClass('activated').next().addClass('active');
		            $(".form-wizard-steps .form-wizard-step.active").addClass("activated");
		            $(".form-wizard-steps .form-wizard-step.active").removeClass("active");
		            current_active_step.next().removeClass("activated");
		            current_active_step.next().addClass("active");
		            // progress bar
		            //bar_progress(progress_line, 'right');
		            var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
		            var total_steps = $(".form-wizard-steps .form-wizard-step").length;
		            $(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");
		            $(".form-wizard .form-wizard-progress-line").attr("style", "width:" + ((completed_steps / total_steps) * 100) + "%;");

		            // show next step
		            $(this).next().fadeIn();
		            // scroll window to beginning of the form
		            scroll_to_class($('.form-wizard'), 20);
		        });
		    }
		});

	    //Attachements
		$('#Step7_Skill').on('click', function () {

		    var parent_fieldset = $(this).parents('fieldset');
		    var next_step = true;
		    // navigation steps / progress steps
		    var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
		    var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');

		    // fields validation
		    parent_fieldset.find('.required').each(function () {
		        if ($(this).val() == "") {
		            $(this).addClass('input-error');
		            next_step = true;
		        }
		        else {
		            $(this).removeClass('input-error');
		        }
		    });

		    // fields validation   
		    if (validateStep7() == false)
		        next_step = false;
		    else if (onFinishCallback() == false)
		        next_step = false;
		    else
		        next_step = true;

		    //if (next_step) {
		    //    window.location.href = "/Dashboard/Dashboard";
		    //}
		});

		// previous step
		//$('.form-wizard .btn-previous').on('click', function() {
        $('body').on('click', '.form-wizard .btn-previous', function () {      
			// navigation steps / progress steps
			var current_active_step = $(this).parents('.form-wizard').find('.form-wizard-step.active');
			var progress_line = $(this).parents('.form-wizard').find('.form-wizard-progress-line');
			
			$(this).parents('fieldset').fadeOut(400, function() {
				// change icons
				//current_active_step.removeClass('active').prev().removeClass('activated').addClass('active');
				$(".form-wizard-steps .form-wizard-step.active").addClass("activated");
				$(".form-wizard-steps .form-wizard-step.active").removeClass("active");
				current_active_step.prev().removeClass("activated");
				current_active_step.prev().addClass("active");
				// progress bar
				//bar_progress(progress_line, 'left');
				var completed_steps = $(".form-wizard-steps .form-wizard-step.activated").length;
				var total_steps = $(".form-wizard-steps .form-wizard-step").length;
				$(".form-wizard .progress-bar.progress-bar-striped").attr("style", "width:"+((completed_steps/total_steps)*100)+"%;");
				$(".form-wizard .form-wizard-progress-line").attr("style", "width:"+((completed_steps/total_steps)*100)+"%;");
				// show previous step
				$(this).prev().fadeIn();
				// scroll window to beginning of the form
				scroll_to_class( $('.form-wizard'), 20 );
			});
		});
		
		// submit
		$('.form-wizard').on('submit', function(e) {
			
			// fields validation
			$(this).find('.required').each(function() {
				if( $(this).val() == "" ) {
					e.preventDefault();
					$(this).addClass('input-error');
				}
				else {
					$(this).removeClass('input-error');
				}
			});
			// fields validation
			
		});
		
		
	});
	

	// image uploader scripts 

	var $dropzone = $('.image_picker'),
		$droptarget = $('.drop_target'),
		$dropinput = $('#inputFile'),
		$dropimg = $('.image_preview'),
		$remover = $('[data-action="remove_current_image"]');

	$dropzone.on('dragover', function() {
	  $droptarget.addClass('dropping');
	  return false;
	});

	$dropzone.on('dragend dragleave', function() {
	  $droptarget.removeClass('dropping');
	  return false;
	});

	$dropzone.on('drop', function(e) {
	  $droptarget.removeClass('dropping');
	  $droptarget.addClass('dropped');
	  $remover.removeClass('disabled');
	  e.preventDefault();
	  
	  var file = e.originalEvent.dataTransfer.files[0],
		  reader = new FileReader();

	  reader.onload = function(event) {
		$dropimg.css('background-image', 'url(' + event.target.result + ')');
	  };
	  
	  console.log(file);
	  reader.readAsDataURL(file);

	  return false;
	});

	$dropinput.change(function(e) {
	  $droptarget.addClass('dropped');
	  $remover.removeClass('disabled');
	  $('.image_title input').val('');
	  
	  var file = $dropinput.get(0).files[0],
		  reader = new FileReader();
	  
	  reader.onload = function(event) {
		$dropimg.css('background-image', 'url(' + event.target.result + ')');
	  }
	  
	  reader.readAsDataURL(file);
	});

	$remover.on('click', function() {
	  $dropimg.css('background-image', '');
	  $droptarget.removeClass('dropped');
	  $remover.addClass('disabled');
	  $('.image_title input').val('');
	});

	$('.image_title input').blur(function() {
	  if ($(this).val() != '') {
		$droptarget.removeClass('dropped');
	  }
	});

	// image uploader scripts
	
	
	
}( jQuery ));