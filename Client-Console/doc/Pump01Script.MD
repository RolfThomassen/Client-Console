:----------------
: Short Script of happy pump 01
:----------------
> M01
> FE
> A0101002500002000
> B01EHM$VPL
> B01C
:----------------
: STX = 0x02 = [alt]-2 = ☻
: ETX = 0x03 = [alt]-3 = ♥
:----------------
:
: Long description of Pump 01 Story line description
:
: Start with "Request Pump Status"
> ☻FE♥
< ☻2802020202020202020;0?12038200000000000000000000000000000000000000♥W
:    ^^-- Pump01 status - Idle
: Send Reserve Pump 01
> ☻M01♥
: Send "Request Pump Status"
> ☻FE♥
< ☻280#020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - Reserved
: If Pump01 Status = 0#
: Send Authorize pump
> ☻A0101002500002000♥
: Send "Request Pump Status"
> ☻FE♥
< ☻280:020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - Authorized
> ☻FE♥
< ☻280;020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - NozzlePickUp
> ☻FE♥
< ☻280?020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - Dispensing
> ☻FE♥
< ☻2812020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - DispenseCompleted
: If Pump01 Status = 12
: Send Finalized
> ☻B01EHM$VPL♥
< ☻01H0M1$0000002500V0000011364P002200L002500♥m
> ☻FE♥
< ☻2820020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - Idle
: Optional Send
: Send Cancel
> ☻B01C♥
> ☻FE♥
< ☻2820020202020202020;0?12038200000000000000000000000000000000000000♥f
:    ^^-- Pump01 status - Idle

