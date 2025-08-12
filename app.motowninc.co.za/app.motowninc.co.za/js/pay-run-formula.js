$(document).on('input',
    '.PayRate, .NormalTimeHours, .NormalTimeAmount, .OverTimeHours, .OverTimeAmount, .DoubleTimeHours, .DoubleTimeAmount, .TravellingTimeHours, .TravellingTimeAmount, .LOADays, .LOARate, .LOAAmount, .GrossPay, .UIF, .PAYE, .Deductions, .NettPay'
    , function () {
        var row = $(this).closest('.repeater-row');

        var PayRate = parseFloat(row.find('.PayRate').val()) || 0;

        var NormalTimeHours = parseFloat(row.find('.NormalTimeHours').val()) || 0;
        var NormalTimeAmount = PayRate * NormalTimeHours;
        row.find('.NormalTimeAmount').val(NormalTimeAmount.toFixed(2));

        var OverTimeHours = parseFloat(row.find('.OverTimeHours').val()) || 0;
        var OverTimeAmount = (PayRate * OverTimeHours) * 1.5;
        row.find('.OverTimeAmount').val(OverTimeAmount.toFixed(2));

        var DoubleTimeHours = parseFloat(row.find('.DoubleTimeHours').val()) || 0;
        var DoubleTimeAmount = (PayRate * DoubleTimeHours) * 2
        row.find('.DoubleTimeAmount').val(DoubleTimeAmount.toFixed(2));

        var TravellingTimeHours = parseFloat(row.find('.TravellingTimeHours').val()) || 0;
        var TravellingTimeAmount = PayRate * TravellingTimeHours;
        row.find('.TravellingTimeAmount').val(TravellingTimeAmount.toFixed(2));

        var LOADays = parseFloat(row.find('.LOADays').val()) || 0;
        var LOARate = parseFloat(row.find('.LOARate').val()) || 0;
        var LOAAmount = LOADays * LOARate
        row.find('.LOAAmount').val(LOAAmount.toFixed(2));

        var GrossPay = NormalTimeAmount
            + OverTimeAmount
            + DoubleTimeAmount
            + TravellingTimeAmount
            + LOAAmount

        row.find('.GrossPay').val(GrossPay.toFixed(2));

        var UIF = GrossPay * 0.01;

        if (UIF > 177.12) {
            UIF = 177.12;
        }

        row.find('.UIF').val(UIF.toFixed(2));
        var PAYE = parseFloat(row.find('.PAYE').val()) || 0;
        var Deductions = parseFloat(row.find('.Deductions').val()) || 0;
        var NettPay = GrossPay - (UIF + PAYE + Deductions);
        row.find('.NettPay').val(NettPay.toFixed(2));

        // Calculate totals across all repeater rows
        var totalGrossPay = 0;
        var totalUIF = 0;
        var totalPAYE = 0;
        var totalDeductions = 0;
        var totalNettPay = 0;

        $('.repeater-row').each(function () {
            var gross = parseFloat($(this).find('.GrossPay').val()) || 0;
            var uif = parseFloat($(this).find('.UIF').val()) || 0;
            var paye = parseFloat($(this).find('.PAYE').val()) || 0;
            var deductions = parseFloat($(this).find('.Deductions').val()) || 0;
            var nett = parseFloat($(this).find('.NettPay').val()) || 0;

            totalGrossPay += gross;
            totalUIF += uif;
            totalPAYE += paye;
            totalDeductions += deductions;
            totalNettPay += nett;
        });

        // Display the totals somewhere on the page (you'll need these HTML elements)
        $('.TotalGrossPay').text(totalGrossPay.toFixed(2));
        $('.TotalUIF').text(totalUIF.toFixed(2));
        $('.TotalPAYE').text(totalPAYE.toFixed(2));
        $('.TotalDeductions').text(totalDeductions.toFixed(2));
        $('.TotalNettPay').text(totalNettPay.toFixed(2));
    });