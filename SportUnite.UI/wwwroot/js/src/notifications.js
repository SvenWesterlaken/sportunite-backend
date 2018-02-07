(function($) {

  $(document).ready(function() {
    var notification = $(".notification-hidden-container");

    if(notification.length) {
      Materialize.toast(notification.data("message"), 4000);
    }
  });

})(jQuery);
