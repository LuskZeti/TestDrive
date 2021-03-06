﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("tb_notificacao")]
    public partial class TbNotificacao
    {
        [Key]
        [Column("id_notificacao")]
        public int IdNotificacao { get; set; }
        [Column("id_login")]
        public int IdLogin { get; set; }
        [Required]
        [Column("ds_mensagem", TypeName = "varchar(300)")]
        public string DsMensagem { get; set; }
        [Required]
        [Column("ds_status", TypeName = "varchar(255)")]
        public string DsStatus { get; set; }
        [Column("dt_envio", TypeName = "datetime")]
        public DateTime DtEnvio { get; set; }

        [ForeignKey(nameof(IdLogin))]
        [InverseProperty(nameof(TbLogin.TbNotificacao))]
        public virtual TbLogin IdLoginNavigation { get; set; }
    }
}
